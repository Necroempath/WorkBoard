using MediatR;
using WorkBoard.Application.Features.DTOs;

namespace WorkBoard.Application.Features.Auth;

public record RegisterCommand(RegisterRequest Request) : IRequest<AuthResponseDto>;
