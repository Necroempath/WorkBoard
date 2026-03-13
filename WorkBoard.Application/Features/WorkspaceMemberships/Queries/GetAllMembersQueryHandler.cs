using AutoMapper;
using MediatR;
using WorkBoard.Application.Abstractions;
using WorkBoard.Application.Abstractions.Repositories;
using WorkBoard.Domain.Extensions;

namespace WorkBoard.Application.Features.WorkspaceMemberships.Queries;

public sealed class GetAllMembersQueryHandler : IRequestHandler<GetAllMembersQuery,  IEnumerable<WorkspaceMembershipResponseDto>>
{
    private readonly IWorkspaceMembershipRepository _membershipRepository;
    private readonly ICurrentWorkspaceService _currentWorkspace;
    private readonly IMapper _mapper;

    public GetAllMembersQueryHandler(IWorkspaceMembershipRepository membershipRepository, ICurrentWorkspaceService currentWorkspace, IMapper mapper)
    {
        _membershipRepository = membershipRepository;
        _currentWorkspace = currentWorkspace;
        _mapper = mapper;
    }

    public async Task<IEnumerable<WorkspaceMembershipResponseDto>> Handle(GetAllMembersQuery request, CancellationToken ct)
    {
        if (!_currentWorkspace.Membership.Role.CanViewMembers())
            throw new UnauthorizedAccessException("Members, Admins or Owner only allowed for this action");

        var members = await _membershipRepository.GetMembersAsync(_currentWorkspace.WorkspaceId, ct);

        return _mapper.Map<IEnumerable<WorkspaceMembershipResponseDto>>(members);
    }
}