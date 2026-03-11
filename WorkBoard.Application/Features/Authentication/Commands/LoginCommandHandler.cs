using AutoMapper;
using MediatR;
using WorkBoard.Application.Abstractions.Repositories;
using WorkBoard.Application.Abstractions;

namespace WorkBoard.Application.Features.Authentication.Commands;

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponseDto>
{
    private readonly IUserRepository _repository;
    private readonly IPasswordHasher _hasher;
    private readonly IJwtTokenGenerator _jwtGenerator;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;
    private readonly IMapper _mapper;

    public LoginCommandHandler(IUserRepository repository, IPasswordHasher hasher, 
        IRefreshTokenGenerator refreshTokenGenerator, IJwtTokenGenerator jwtGenerator, IMapper mapper)
    {
        _repository = repository;
        _hasher = hasher;
        _jwtGenerator = jwtGenerator;
        _refreshTokenGenerator = refreshTokenGenerator;
        _mapper = mapper;
    }

    public async Task<AuthResponseDto> Handle(LoginCommand request, CancellationToken token)
    {
        var user = await _repository.GetByEmailAsync(request.Request.Email, token);

        if (user is null)
            throw new ArgumentException("Invalid email or password");

        if (!_hasher.Verify(request.Request.Password, user.PasswordHash))
            throw new ArgumentException("Invalid email or password");

        return AuthorizationHelper.SetUpTokens(user, _jwtGenerator, _refreshTokenGenerator, _mapper);
    }
}