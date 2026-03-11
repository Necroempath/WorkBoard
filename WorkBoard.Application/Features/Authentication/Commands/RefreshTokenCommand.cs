using MediatR;

namespace WorkBoard.Application.Features.Authentication.Commands;

public record RefreshTokenCommand(RefreshTokenRequest Request) : IRequest<RefreshResponseDto>;
