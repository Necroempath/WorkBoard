using WorkBoard.Domain.Enums;

namespace WorkBoard.Application.Features.WorkspaceMemberships;

public sealed class WorkspaceMembershipResponseDto
{
    public Guid MemberId { get; set; }
    public string MemberName { get; set; } = string.Empty;
    public string MemberEmail { get; set; } = string.Empty;
    public WorkspaceRole MemberRole { get; set; }
    public DateTimeOffset JoinedAt { get; set; }
}
