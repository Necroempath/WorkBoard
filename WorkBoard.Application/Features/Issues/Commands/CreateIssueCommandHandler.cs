using AutoMapper;
using MediatR;
using WorkBoard.Application.Abstractions;
using WorkBoard.Application.Abstractions.Repositories;
using WorkBoard.Domain.Entities;
using WorkBoard.Domain.Extensions;

namespace WorkBoard.Application.Features.Issues.Commands;

public sealed class CreateIssueCommandHandler : IRequestHandler<CreateIssueCommand, IssueResponseDto>
{
    private readonly IIssueRepository _issueRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly ICurrentWorkspaceService _currentWorkspace;
    private readonly IMapper _mapper;

    public CreateIssueCommandHandler(IIssueRepository issueRepository,
        IProjectRepository projectRepository, ICurrentWorkspaceService currentWorkspace, IMapper mapper)
    {
        _issueRepository = issueRepository;
        _projectRepository = projectRepository;
        _currentWorkspace = currentWorkspace;
        _mapper = mapper;
    }

    public async Task<IssueResponseDto> Handle(CreateIssueCommand command, CancellationToken ct)
    {
        if (!_currentWorkspace.Membership.Role.CanManageProjects())
            throw new InvalidOperationException("Only Owner or Admins can create issues");

        Issue issue = _mapper.Map<Issue>(command.Request);

        var column = await _projectRepository.GetColumnByIdAsync(command.ColumnId, ct)
            ?? throw new InvalidOperationException("Invalid column Id");

        column.AddIssue(command.Request.Title, command.Request.Description, command.Request.Priority, command.Request.ProjectId);

        await _projectRepository.SaveAsync(ct);

        return _mapper.Map<IssueResponseDto>(issue);
    }
}
