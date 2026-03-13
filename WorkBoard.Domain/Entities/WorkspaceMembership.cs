using WorkBoard.Domain.Enums;

namespace WorkBoard.Domain.Entities;

public sealed class WorkspaceMembership
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public User User { get; private set; } = null!;
    public Guid WorkspaceId { get; private set; }
    public Workspace Workspace { get; private set; } = null!;

    public WorkspaceRole Role { get; set; }
    public DateTimeOffset JoinedAt { get; private set; }

    private WorkspaceMembership() { }

    public WorkspaceMembership(Guid userId, Guid workspaceId, WorkspaceRole role, DateTimeOffset joinedAt)
    {
        UserId = userId;
        WorkspaceId = workspaceId;
        Role = role;
        JoinedAt = joinedAt;
    }


}
