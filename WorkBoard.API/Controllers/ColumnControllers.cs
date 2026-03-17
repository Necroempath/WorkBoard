using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkBoard.Application.Features.Projects.Commands;
using WorkBoard.Application.Features.Projects;
using WorkBoard.Application.Features.Workspaces;
using WorkBoard.Application.Features.Projects.Queries;
using WorkBoard.Application.Features.Columns;
using WorkBoard.Application.Features.Columns.Commands;

namespace WorkBoard.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class ColumnControllers : ControllerBase
{
    private readonly IMediator _mediator;

    public ColumnControllers(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpPost]
    [Authorize]
    public async Task<ActionResult<WorkspaceResponseDto>> Add([FromBody] CreateColumnRequest dto, CancellationToken token)
    {
        return Ok(await _mediator.Send(new AddColumnCommand(dto.Name, dto.ProjectId), token));
    }
}