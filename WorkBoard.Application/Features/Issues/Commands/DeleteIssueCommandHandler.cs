using MediatR;
using WorkBoard.Application.Abstractions.Repositories;
using WorkBoard.Application.Abstractions;
using WorkBoard.Domain.Extensions;

namespace WorkBoard.Application.Features.Issues.Commands;

public sealed record DeleteIssueCommandHandler : IRequestHandler<DeleteIssueCommand, bool>
{
    private readonly IIssueRepository _issueRepository;
    private readonly ICurrentWorkspaceService _currentWorkspace;

    public DeleteIssueCommandHandler(IIssueRepository issueRepository, ICurrentWorkspaceService currentWorkspace)
    {
        _issueRepository = issueRepository;
        _currentWorkspace = currentWorkspace;
    }

    public async Task<bool> Handle(DeleteIssueCommand request, CancellationToken ct)
    {
        if (!_currentWorkspace.Membership.Role.CanManageProjects())
            throw new InvalidOperationException("Only Owner or Admins can delete issues");

        await _issueRepository.DeleteAsync(request.IssueId, ct);

        return true;
    }
}