using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkBoard.Application.Features.Workspaces;
using WorkBoard.Application.Features.Workspaces.CreateWorkspace;
using WorkBoard.Application.Features.Workspaces.Queries;

namespace WorkBoard.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public sealed class WorkspaceControllers : ControllerBase
{
    private readonly IMediator _mediator;

    public WorkspaceControllers(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<WorkspaceResponseDto>>> GetAll(CancellationToken token)
    {
        return Ok(await _mediator.Send(new GetAllWorkspacesQuery(), token));
    }

    [HttpPost]
    public async Task<ActionResult<WorkspaceResponseDto>> Create([FromBody]CreateWorkspaceRequest dto, CancellationToken token)
    {
        return Ok(await _mediator.Send(new CreateWorkspaceCommand(dto), token));
    }
}
