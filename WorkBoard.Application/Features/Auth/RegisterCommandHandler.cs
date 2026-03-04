using AutoMapper;
using MediatR;
using WorkBoard.Application.Features.DTOs;
using WorkBoard.Application.Interfaces;
using WorkBoard.Domain;

namespace WorkBoard.Application.Features.Auth;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponseDto>
{
    private readonly IUserRepository _repository;
    private readonly IPasswordHasher _hasher;
    private readonly IJwtTokenGenerator _generator;
    private readonly IMapper _mapper;

    public RegisterCommandHandler(IUserRepository repository, IPasswordHasher hasher, IJwtTokenGenerator generator, IMapper mapper)
    {
        _repository = repository;
        _hasher = hasher;
        _generator = generator;
        _mapper = mapper;
    }

    public async Task<AuthResponseDto> Handle(RegisterCommand command, CancellationToken token)
    {
        var hash = _hasher.Hash(command.Request.Password);

        User user = new(command.Request.Name, command.Request.Email, hash);

        await _repository.AddAsync(user, token);

        var jwt = _generator.Generate(user);

        var authResponseDto = _mapper.Map<AuthResponseDto>(user);

        authResponseDto.Jwt = jwt;

        return authResponseDto;
    }
}