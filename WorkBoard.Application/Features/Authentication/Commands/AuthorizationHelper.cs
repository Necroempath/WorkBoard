using AutoMapper;
using WorkBoard.Application.Abstractions;
using WorkBoard.Domain;

namespace WorkBoard.Application.Features.Authentication.Commands;

public sealed class AuthorizationHelper
{
    public static AuthResponseDto SetUpTokens(User user, IJwtTokenGenerator jwtTokenGenerator, IRefreshTokenGenerator refreshTokenGenerator, IMapper mapper)
    {
        var refreshToken = refreshTokenGenerator.Generate(user);

        var jwt = jwtTokenGenerator.Generate(user);

        user.AddToken(refreshToken);

        var authResponseDto = mapper.Map<AuthResponseDto>(user);

        authResponseDto.Jwt = jwt;

        return authResponseDto;
    }
}
