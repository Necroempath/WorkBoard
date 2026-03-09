using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkBoard.Application.DTOs;
using WorkBoard.Application.Features.Workspaces.CreateWorkspace;
using WorkBoard.Application.Features.Workspaces.Queries;
using WorkBoard.Domain;

namespace WorkBoard.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class WorkspaceControllers : ControllerBase
{
    private readonly IMediator _mediator;

    public WorkspaceControllers(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Workspace>>> GetAll(CancellationToken token)
    {
        return Ok(await _mediator.Send(new GetAllWorkspacesQuery(), token));
    }

    [HttpPost]
    public async Task<ActionResult<Workspace>> Create([FromBody]CreateWorkspaceRequest dto, CancellationToken token)
    {
        return Ok(await _mediator.Send(new CreateWorkspaceCommand(dto), token));
    }
}
