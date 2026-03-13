using MediatR;

namespace WorkBoard.Application.Features.WorkspaceMemberships.Queries;

public sealed record GetAllMembersQuery(Guid WorkspaceId) : IRequest<IEnumerable<WorkspaceMembershipResponseDto>>;
