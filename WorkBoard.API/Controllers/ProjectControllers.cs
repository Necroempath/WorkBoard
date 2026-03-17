using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkBoard.Application.Features.Projects;
using WorkBoard.Application.Features.Projects.Commands;
using WorkBoard.Application.Features.Projects.Queries;
using WorkBoard.Application.Features.Workspaces;

namespace WorkBoard.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class ProjectControllers : ControllerBase
{
    private readonly IMediator _mediator;

    public ProjectControllers(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{workspaceId}")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<ProjectResponseDto>>> GetAll(Guid workspaceId, CancellationToken token)
    {
        return Ok(await _mediator.Send(new GetAllProjectsQuery(workspaceId), token));
    }

    [HttpGet("{workspaceId}/{projectId}")]
    [Authorize]
    public async Task<ActionResult<ProjectResponseDto>> GetById(Guid workspaceId, Guid projectId, CancellationToken token)
    {
        return Ok(await _mediator.Send(new GetProjectByIdQuery(projectId, workspaceId), token));
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<WorkspaceResponseDto>> Create([FromBody] CreateProjectRequest dto, CancellationToken token)
    {
        return Ok(await _mediator.Send(new CreateProjectCommand(dto, dto.WorkspaceId), token));
    }

    [HttpDelete]
    [Authorize]
    public async Task<ActionResult<WorkspaceResponseDto>> Delete([FromBody]Guid projectId, CancellationToken token)
    {
        return Ok(await _mediator.Send(new DeleteProjectCommand(projectId), token));
    }
}