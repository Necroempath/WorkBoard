using MediatR;

namespace WorkBoard.Application.Features.Workspaces.CreateWorkspace;

public sealed record CreateWorkspaceCommand(CreateWorkspaceDto Dto) : IRequest<WorkspaceResponseDto>;
