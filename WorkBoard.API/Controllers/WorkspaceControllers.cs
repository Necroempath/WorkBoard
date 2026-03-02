using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkBoard.Application.Features.Workspaces.CreateWorkspace;
using WorkBoard.Domain;

namespace WorkBoard.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkspaceControllers : ControllerBase
{
    private readonly IMediator _mediator;

    public WorkspaceControllers(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Workspace>>> GetAll(CancellationToken token)
    {
        return Ok(await _mediator.Send(token));
    }

    [HttpPost]
    public async Task<ActionResult<Workspace>> Create([FromBody]CreateWorkspaceCommand command, CancellationToken token)
    {
        return Ok(await _mediator.Send(command, token));
    }
}
