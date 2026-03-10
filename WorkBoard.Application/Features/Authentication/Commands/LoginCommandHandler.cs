using AutoMapper;
using MediatR;
using WorkBoard.Application.Abstractions.Repositories;
using WorkBoard.Application.Interfaces;

namespace WorkBoard.Application.Features.Authentication.Commands;

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponseDto>
{
    private readonly IUserRepository _repository;
    private readonly IPasswordHasher _hasher;
    private readonly IJwtTokenGenerator _generator;
    private readonly IMapper _mapper;

    public LoginCommandHandler(IUserRepository repository, IPasswordHasher hasher, IJwtTokenGenerator generator, IMapper mapper)
    {
        _repository = repository;
        _hasher = hasher;
        _generator = generator;
        _mapper = mapper;
    }

    public async Task<AuthResponseDto> Handle(LoginCommand request, CancellationToken token)
    {
        var user = await _repository.GetByEmailAsync(request.Request.Email, token);

        if (user is null)
            throw new ArgumentException("Invalid email or password");

        if (!_hasher.Verify(request.Request.Password, user.PasswordHash))
            throw new ArgumentException("Invalid email or password");

        var jwt = _generator.Generate(user);

        var authResponseDto = _mapper.Map<AuthResponseDto>(user);

        authResponseDto.Jwt = jwt;

        return authResponseDto;
    }
}