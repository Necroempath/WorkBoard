using AutoMapper;
using MediatR;
using WorkBoard.Application.Abstractions.Repositories;

namespace WorkBoard.Application.Features.WorkspaceMemberships.Queries;

public sealed class GetAllMembersQueryHandler : IRequestHandler<GetAllMembersQuery,  IEnumerable<WorkspaceMembershipResponseDto>>
{
    private readonly IWorkspaceMembershipRepository _membershipRepository;
    private readonly IMapper _mapper;

    public GetAllMembersQueryHandler(IWorkspaceRepository workspaceRepository, IWorkspaceMembershipRepository membershipRepository, IMapper mapper)
    {
        _membershipRepository = membershipRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<WorkspaceMembershipResponseDto>> Handle(GetAllMembersQuery request, CancellationToken ct)
    {
        var members = await _membershipRepository.GetMembersAsync(request.WorkspaceId, ct);

        return _mapper.Map<IEnumerable<WorkspaceMembershipResponseDto>>(members);
    }
}