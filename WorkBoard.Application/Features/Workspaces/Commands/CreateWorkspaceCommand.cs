using MediatR;

namespace WorkBoard.Application.Features.Workspaces.CreateWorkspace;

public sealed record CreateWorkspaceCommand(string Name, Guid OwnerId) : IRequest<WorkspaceResponseDto>;
