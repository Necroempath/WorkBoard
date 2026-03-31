using AutoMapper;
using MediatR;
using WorkBoard.Application.Abstractions;
using WorkBoard.Application.Abstractions.Repositories;
using WorkBoard.Domain.Extensions;

namespace WorkBoard.Application.Features.Issues.Commands;

public sealed class MoveIssueCommandHandler : IRequestHandler<MoveIssueCommand, IssueResponseDto>
{
    private readonly ICurrentWorkspaceService _currentWorkspace;
    private readonly IIssueRepository _issueRepository;
    private readonly IMapper _mapper;

    public MoveIssueCommandHandler(ICurrentWorkspaceService currentWorkspace, IIssueRepository issueRepository, IMapper mapper)
    {
        _currentWorkspace = currentWorkspace;
        _issueRepository = issueRepository;
        _mapper = mapper;
    }

    public async Task<IssueResponseDto> Handle(MoveIssueCommand command, CancellationToken ct)
    {
        if (!_currentWorkspace.Membership.Role.CanMoveIssues())
            throw new InvalidOperationException("Viewers can not move issues");

        var issue = await _issueRepository.GetByIdAsync(command.IssueId, ct)
          ?? throw new InvalidOperationException("Issue not found");

        issue.SetColumnId(command.Request.TargetColumnId);

        await _issueRepository.SaveAsync(ct);

        return _mapper.Map<IssueResponseDto>(issue);
    }
}
