using MediatR;
using WorkBoard.Application.Abstractions;
using WorkBoard.Application.Abstractions.Repositories;

namespace WorkBoard.Application.Behaviors;

public class WorkspaceMembershipBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly IWorkspaceMembershipRepository _repo;
    private readonly ICurrentWorkspaceService _currentWorkspace;
    private readonly ICurrentUserService _currentUser;

    public WorkspaceMembershipBehavior(IWorkspaceMembershipRepository repo, ICurrentWorkspaceService workspace, ICurrentUserService user)
    {
        _repo = repo;
        _currentWorkspace = workspace;
        _currentUser = user;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
    {
        if (request is IWorkspaceRequest workspaceRequest)
        {
            var membership = await _repo.GetMembershipAsync(_currentUser.UserId, workspaceRequest.WorkspaceId, ct);

            if (membership is null)
                throw new InvalidOperationException("Desired workspace not found");

            _currentWorkspace.WorkspaceId = workspaceRequest.WorkspaceId;
            _currentWorkspace.Membership = membership;
        }

        return await next();
    }
}