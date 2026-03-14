using WorkBoard.Domain.Enums;

namespace WorkBoard.Domain.Extensions;

public static class DomainExtensions
{
    public static bool CanManageMembers(this WorkspaceRole role) => role is WorkspaceRole.Owner or WorkspaceRole.Admin;

    public static bool CanViewMembers(this WorkspaceRole role) => role is WorkspaceRole.Owner or WorkspaceRole.Admin or WorkspaceRole.Member;

    public static bool CanDeleteMembers(this WorkspaceRole role) => role is WorkspaceRole.Owner;
}
