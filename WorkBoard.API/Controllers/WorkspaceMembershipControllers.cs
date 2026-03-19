using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkBoard.Application.Features.WorkspaceMemberships;
using WorkBoard.Application.Features.WorkspaceMemberships.Commands;
using WorkBoard.Application.Features.WorkspaceMemberships.Queries;

namespace WorkBoard.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public sealed class WorkspaceMembershipControllers : ControllerBase
{
    private readonly IMediator _mediator;

    public WorkspaceMembershipControllers(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<WorkspaceMembershipResponseDto>>> GetAllMembers(Guid workspaceId, CancellationToken token)
    {
        return Ok(await _mediator.Send(new GetAllMembersQuery(workspaceId), token));
    }

    [HttpPost("{workspaceId}")]
    public async Task<ActionResult<WorkspaceMembershipResponseDto>> AddMember([FromBody]AddMemberRequest dto, Guid workspaceId, CancellationToken token)
    {
        return Ok(await _mediator.Send(new AddMemberCommand(dto, workspaceId), token));
    }

    [HttpDelete("{workspaceId}")]
    public async Task<ActionResult<bool>> RemoveMember([FromBody]Guid memberId, Guid workspaceId, CancellationToken token)
    {
        return Ok(await _mediator.Send(new RemoveMemberCommand(memberId, workspaceId), token));
    }
}