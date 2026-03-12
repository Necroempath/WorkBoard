using WorkBoard.Domain.Enums;

namespace WorkBoard.Domain.Extensions;

public static class DomainExtensions
{
    public static bool CanManageMembers(this WorkspaceRole role)
    {
        return role is WorkspaceRole.Owner or WorkspaceRole.Admin;
    }
}
