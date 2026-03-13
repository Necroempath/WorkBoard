using WorkBoard.Domain.Entities;
using WorkBoard.Domain.Enums;

namespace WorkBoard.Application.Abstractions.Repositories;

public interface IWorkspaceMembershipRepository
{
    Task<IEnumerable<WorkspaceMembership>> GetMembersAsync(Guid workspaceId, CancellationToken ct);
    Task<IEnumerable<WorkspaceMembership>> GetWorkspacesAsync(Guid userId, CancellationToken ct);
    Task<WorkspaceMembership?> GetMembershipAsync(Guid userId, Guid workspaceId, CancellationToken ct);
    Task<WorkspaceMembership> AddMemberAsync(WorkspaceMembership member, CancellationToken ct); 
    Task<bool> RemoveMemberAsync(Guid id, CancellationToken ct);
    Task<WorkspaceMembership?> ChangeRoleAsync(Guid id, WorkspaceRole role, CancellationToken ct);
}
