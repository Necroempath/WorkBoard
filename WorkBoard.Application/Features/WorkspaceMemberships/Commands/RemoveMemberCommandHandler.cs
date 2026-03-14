using AutoMapper;
using MediatR;
using WorkBoard.Application.Abstractions.Repositories;
using WorkBoard.Application.Abstractions;
using WorkBoard.Domain.Entities;
using WorkBoard.Domain.Extensions;

namespace WorkBoard.Application.Features.WorkspaceMemberships.Commands;

public sealed class RemoveMemberCommandHandler : IRequestHandler<RemoveMemberCommand, bool>
{
    private readonly IWorkspaceMembershipRepository _membershipRepository;
    private readonly ICurrentWorkspaceService _currentWorkspace;
    private readonly IMapper _mapper;

    public RemoveMemberCommandHandler(IWorkspaceMembershipRepository membershipRepository,
        ICurrentWorkspaceService currentWorkspace, IMapper mapper)
    {
        _membershipRepository = membershipRepository;
        _currentWorkspace = currentWorkspace;
        _mapper = mapper;
    }

    public async Task<bool> Handle(RemoveMemberCommand command, CancellationToken ct)
    {
        if (!_currentWorkspace.Membership.Role.CanDeleteMembers())
            throw new InvalidOperationException("Don't have permission to delete members");

        var isDeleted = await _membershipRepository.RemoveMemberAsync(command.MemberId, _currentWorkspace.WorkspaceId, ct);

        if (!isDeleted)
            throw new InvalidOperationException("Invalid Member Id");

        return isDeleted;
    }
}
