using MediatR;

namespace WorkBoard.Application.Features.Workspaces.Queries;

public record GetAllWorkspacesQueries : IRequest<IEnumerable<WorkspaceResponseDto>>;