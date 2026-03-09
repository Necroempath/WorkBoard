using MediatR;
using WorkBoard.Application.DTOs;

namespace WorkBoard.Application.Features.Authentication;

public record RegisterCommand(RegisterRequest Request) : IRequest<AuthResponseDto>;
