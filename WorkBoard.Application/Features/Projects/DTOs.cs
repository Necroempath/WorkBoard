using WorkBoard.Application.Features.Columns;

namespace WorkBoard.Application.Features.Projects;

public sealed class ProjectResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ColumnResponseDto[] Columns { get; set; } = null!;
}

public sealed class CreateProjectRequest
{
    public Guid WorkspaceId { get; set; }
    public string Name { get; set; } = string.Empty;
}