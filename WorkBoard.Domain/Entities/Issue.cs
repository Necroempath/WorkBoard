using WorkBoard.Domain.Enums;

namespace WorkBoard.Domain.Entities;

public sealed class Issue : BaseEntity
{
    public string Title { get; private set; } = string.Empty;
    public IssuePriority Priority { get; set; }
    public string? Description { get; private set; }
    public int Order { get; private set; }

    public Guid ColumnId { get; private set; }
    public Column Column { get; private set; }
    public Guid ProjectId { get; private set; }
    public Project Project { get; private set; }

    private Issue() { }

    internal Issue(string title, Guid columnId, Guid projectId, int order, IssuePriority priority = IssuePriority.Medium, string? description = null)
    {
        SetTitle(title);
        SetDescription(description);
        SetColumnId(columnId);
        SetOrder(order);

        Priority = priority;
        ProjectId = projectId;
    }

    public void Rename(string title)
    {
        SetTitle(title);
    }

    public void UpdateDescription(string? description)
    {
        SetDescription(description);
    }

    internal void SetOrder(int order)
    {
        if (order < 0)
            throw new ArgumentException("Issue order cannot be negative");

        Order = order;
    }

    internal void SetColumnId(Guid columnId)
    {
        if (columnId == Guid.Empty)
            throw new ArgumentException("ColumnId cannot be empty");

        ColumnId = columnId;
    }

    private void SetTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Issue title cannot be empty");

        if (title.Length < 3)
            throw new ArgumentException("Issue title must be at least 3 characters long");

        if (title.Length > 200)
            throw new ArgumentException("Issue title cannot exceed 200 characters");

        Title = title;        
    }

    private void SetDescription(string? description)
    {
        Description = string.IsNullOrWhiteSpace(description) 
            ? null 
            : description;
    }
}