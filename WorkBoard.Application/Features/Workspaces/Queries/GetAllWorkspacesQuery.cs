using MediatR;
using WorkBoard.Application.Features.DTOs;

namespace WorkBoard.Application.Features.Workspaces.Queries;

public sealed record GetAllWorkspacesQuery : IRequest<IEnumerable<WorkspaceResponseDto>>;