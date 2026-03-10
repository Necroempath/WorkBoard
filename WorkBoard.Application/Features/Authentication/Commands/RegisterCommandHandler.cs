using AutoMapper;
using MediatR;
using WorkBoard.Application.Abstractions.Repositories;
using WorkBoard.Application.Interfaces;
using WorkBoard.Domain;

namespace WorkBoard.Application.Features.Authentication.Commands;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponseDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IPasswordHasher _hasher;
    private readonly IJwtTokenGenerator _generator;
    private readonly IMapper _mapper;

    public RegisterCommandHandler(IUserRepository userRepository, IRoleRepository roleRepository, IPasswordHasher hasher, IJwtTokenGenerator generator, IMapper mapper)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _hasher = hasher;
        _generator = generator;
        _mapper = mapper;
    }

    public async Task<AuthResponseDto> Handle(RegisterCommand command, CancellationToken token)
    {
        var hash = _hasher.Hash(command.Request.Password);

        User user = new(command.Request.Name, command.Request.Email, hash);

        var userRole = await _roleRepository.GetByNameAsync(Role.User, token) 
            ?? throw new InvalidOperationException($"No role {Role.User} found in storage to make initial role assigning");

        user.AssignRole(userRole);

        await _userRepository.AddAsync(user, token);

        var jwt = _generator.Generate(user);

        var authResponseDto = _mapper.Map<AuthResponseDto>(user);

        authResponseDto.Jwt = jwt;

        return authResponseDto;
    }
}
