using AutoMapper;
using MediatR;
using WorkBoard.Application.Abstractions.Repositories;
using WorkBoard.Application.Abstractions;

namespace WorkBoard.Application.Features.Authentication.Commands;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, RefreshResponseDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IJwtTokenGenerator _jwtGenerator;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;
    private readonly IMapper _mapper;

    public RefreshTokenCommandHandler(IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository,
        IJwtTokenGenerator jwtGenerator, IRefreshTokenGenerator refreshTokenGenerator, IMapper mapper)
    {
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _jwtGenerator = jwtGenerator;
        _refreshTokenGenerator = refreshTokenGenerator;
        _mapper = mapper;
    }

    public async Task<RefreshResponseDto> Handle(RefreshTokenCommand command, CancellationToken ct)
    {
        var refreshToken = await _refreshTokenRepository.GetByTokenAsync(command.Request.Token, ct);
        var user = await _userRepository.GetByIdAsync(command.Request.UserId, ct);

        if (user is null)
            throw new InvalidOperationException("Invalid Refresh Token");


        if (!user.RefreshTokens.Contains(command.Request.Token))
            throw new InvalidOperationException("Invalid Refresh Token");
        //Refresh token не сохраняется, посмотри в логин и регистер хендленры
        var newRefreshToken = _refreshTokenGenerator.Generate(user);
        var jwt = _jwtGenerator.Generate(user);

        return new();
    }
}
