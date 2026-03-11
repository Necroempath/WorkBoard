using AutoMapper;
using MediatR;
using WorkBoard.Application.Abstractions.Repositories;
using WorkBoard.Application.Abstractions;
using WorkBoard.Domain;

namespace WorkBoard.Application.Features.Authentication.Commands;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponseDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IPasswordHasher _hasher;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;
    private readonly IJwtTokenGenerator _jwtGenerator;
    private readonly IMapper _mapper;

    public RegisterCommandHandler(IUserRepository userRepository, IRoleRepository roleRepository, IPasswordHasher hasher,
         IRefreshTokenGenerator refreshTokenGenerator, IJwtTokenGenerator jwtGenerator, IMapper mapper)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _hasher = hasher;
        _jwtGenerator = jwtGenerator;
        _refreshTokenGenerator = refreshTokenGenerator;
        _mapper = mapper;
    }

    public async Task<AuthResponseDto> Handle(RegisterCommand command, CancellationToken ct)
    {
        var hash = _hasher.Hash(command.Request.Password);

        User user = new(command.Request.Name, command.Request.Email, hash);

        var userRole = await _roleRepository.GetByNameAsync(Role.User, ct) 
            ?? throw new InvalidOperationException($"No role {Role.User} found in storage to make initial role assigning");

        var refreshToken = _refreshTokenGenerator.Generate(user);

        user.AssignRole(userRole);

        await _userRepository.AddAsync(user, ct);

        return AuthorizationHelper.SetUpTokens(user, _jwtGenerator, _refreshTokenGenerator, _mapper);
    }
}
