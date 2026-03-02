namespace WorkBoard.Application.Features.Workspaces;

public sealed class WorkspaceResponseDto(Guid Id, string Name, Guid OwnerId);
public sealed class CreateWorkspaceDto(string Name, Guid OwnerId);
public sealed class UpdateWorkspaceDto(string Name);