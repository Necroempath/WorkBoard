namespace WorkBoard.Application.Features.Workspaces.CreateWorkspace;

public sealed record CreateWorkspaceCommand(string Name, Guid OwnerId);
