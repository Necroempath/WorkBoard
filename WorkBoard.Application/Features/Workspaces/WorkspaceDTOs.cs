namespace WorkBoard.Application.Features.Workspaces;

public sealed class WorkspaceResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid OwnerId { get; set; }
}
public sealed class CreateWorkspaceDto
{
    public string Name { get; set; } = string.Empty;
    public Guid OwnerId { get; set; }
}
public sealed class UpdateWorkspaceDto
{
    public string Name { get; set; } = string.Empty;
}