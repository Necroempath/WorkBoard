using MediatR;

namespace WorkBoard.Application.Features.Workspaces.Queries;

public record GetAllWorkspacesQuery : IRequest<IEnumerable<WorkspaceResponseDto>>;