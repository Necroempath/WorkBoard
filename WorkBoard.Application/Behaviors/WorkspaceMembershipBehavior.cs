using MediatR;
using WorkBoard.Application.Abstractions;
using WorkBoard.Application.Abstractions.Repositories;
using WorkBoard.Domain.Entities;

namespace WorkBoard.Application.Behaviors;

public class WorkspaceMembershipBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly IWorkspaceMembershipRepository _membershipRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly ICurrentWorkspaceService _currentWorkspace;
    private readonly ICurrentUserService _currentUser;

    public WorkspaceMembershipBehavior(IWorkspaceMembershipRepository membershipRepository, IProjectRepository projectRepository,
        ICurrentWorkspaceService workspace, ICurrentUserService user)
    {
        _membershipRepository = membershipRepository;
        _projectRepository = projectRepository;
        _currentWorkspace = workspace;
        _currentUser = user;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
    {
        Guid workspaceId;
        WorkspaceMembership membership;

        if (request is IWorkspaceRequest workspaceRequest)
        {
            workspaceId = workspaceRequest.WorkspaceId;

            membership = await _membershipRepository.GetMembershipAsync(_currentUser.UserId, workspaceRequest.WorkspaceId, ct) 
                ?? throw new InvalidOperationException("Invalid Workspace Id");
        }
        else if (request is IProjectRequest projectRequest)
        {
            var project = await _projectRepository.GetByIdAsync(projectRequest.ProjectId, ct) 
                ?? throw new InvalidOperationException("Invalid Project Id");

            workspaceId = project.WorkspaceId;

            membership = await _membershipRepository.GetMembershipAsync(_currentUser.UserId, project.WorkspaceId, ct)
                ?? throw new InvalidOperationException("Invalid Workspace Id");
        }
        else
            return await next(ct);

        _currentWorkspace.WorkspaceId = workspaceId;
        _currentWorkspace.Membership = membership;

        return await next(ct);
    }
}