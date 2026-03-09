using MediatR;
using WorkBoard.Application.Features.Authentication.DTOs;

namespace WorkBoard.Application.Features.Authentication;

public record RegisterCommand(RegisterRequest Request) : IRequest<AuthResponseDto>;
