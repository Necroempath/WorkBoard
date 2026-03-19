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
[Authorize]
public sealed class ProjectControllers : ControllerBase
{
    private readonly IMediator _mediator;

    public ProjectControllers(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{workspaceId}/all")]
    public async Task<ActionResult<IEnumerable<ProjectResponseDto>>> GetAll(Guid workspaceId, CancellationToken token)
    {
        return Ok(await _mediator.Send(new GetAllProjectsQuery(workspaceId), token));
    }

    [HttpGet("{projectId}")]
    public async Task<ActionResult<ProjectResponseDto>> GetById(Guid projectId, CancellationToken token)
    {
        return Ok(await _mediator.Send(new GetProjectByIdQuery(projectId), token));
    }

    [HttpPost]
    public async Task<ActionResult<ProjectResponseDto>> Create([FromBody] CreateProjectRequest dto, CancellationToken token)
    {
        return Ok(await _mediator.Send(new CreateProjectCommand(dto, dto.WorkspaceId), token));
    }

    [HttpDelete]
    public async Task<ActionResult<bool>> Delete([FromBody]Guid projectId, CancellationToken token)
    {
        return Ok(await _mediator.Send(new DeleteProjectCommand(projectId), token));
    }
}