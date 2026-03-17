namespace WorkBoard.Application.Features.Columns;

public sealed class ColumnResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public sealed class CreateColumnRequest
{
    public Guid ProjectId { get; set; }
    public string Name { get; set; } = string.Empty;
}
