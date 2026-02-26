namespace WorkBoard.Application.Features.Workspaces;

public sealed record WorkspaceDto(Guid Id, string Name, Guid OwnerId);