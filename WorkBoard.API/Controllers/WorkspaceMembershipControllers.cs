using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkBoard.Application.Features.WorkspaceMemberships;
using WorkBoard.Application.Features.WorkspaceMemberships.Queries;

namespace WorkBoard.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class WorkspaceMembershipControllers : ControllerBase
{
    private readonly IMediator _mediator;

    public WorkspaceMembershipControllers(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<WorkspaceMembershipResponseDto>>> GetAllMembers(Guid workspaceId, CancellationToken token)
    {
        return Ok(await _mediator.Send(new GetAllMembersQuery(workspaceId), token));
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<WorkspaceMembershipResponseDto>> AddMember([FromBody] CreateWorkspaceRequest dto, CancellationToken token)
    {
        return Ok(await _mediator.Send(new CreateWorkspaceCommand(dto), token));
    }
}