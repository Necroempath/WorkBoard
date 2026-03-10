using AutoMapper;
using MediatR;
using WorkBoard.Application.Abstractions.Repositories;
using WorkBoard.Application.Entities;
using WorkBoard.Application.Interfaces;
using WorkBoard.Domain;

namespace WorkBoard.Application.Features.Authentication.Commands;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponseDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IPasswordHasher _hasher;
    private readonly IJwtTokenGenerator _generator;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IMapper _mapper;

    public RegisterCommandHandler(IUserRepository userRepository, IRoleRepository roleRepository,
        IPasswordHasher hasher, IJwtTokenGenerator generator, IRefreshTokenRepository refreshTokenRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _hasher = hasher;
        _refreshTokenRepository = refreshTokenRepository;
        _generator = generator;
        _mapper = mapper;
    }

    public async Task<AuthResponseDto> Handle(RegisterCommand command, CancellationToken ct)
    {
        var hash = _hasher.Hash(command.Request.Password);

        User user = new(command.Request.Name, command.Request.Email, hash);

        var userRole = await _roleRepository.GetByNameAsync(Role.User, ct) 
            ?? throw new InvalidOperationException($"No role {Role.User} found in storage to make initial role assigning");

        RefreshToken refreshToken = new(new Guid().ToString("N"), user.Id, DateTime.UtcNow.AddDays(7));

        user.AddToken(refreshToken.Token);

        user.AssignRole(userRole);

        await _userRepository.AddAsync(user, ct);

        await _refreshTokenRepository.AddTokenAsync(refreshToken, ct);

        var jwt = _generator.Generate(user);

        var authResponseDto = _mapper.Map<AuthResponseDto>(user);

        authResponseDto.Jwt = jwt;

        return authResponseDto;
    }
}
