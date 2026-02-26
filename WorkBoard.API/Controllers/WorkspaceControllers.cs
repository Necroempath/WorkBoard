using Microsoft.AspNetCore.Mvc;
using WorkBoard.Application.Features.Workspaces.CreateWorkspace;
using WorkBoard.Application.Services;
using WorkBoard.Domain;

namespace WorkBoard.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkspaceControllers : ControllerBase
{
    private readonly WorkspaceService _service;

    public WorkspaceControllers(WorkspaceService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Workspace>>> GetAll()
    {
        return Ok(await _service.GetAllAsync(new CancellationToken()));
    }

    [HttpPost]
    public async Task<ActionResult<IEnumerable<Workspace>>> Create([FromBody]CreateWorkspaceCommand command)
    {
        return Ok(await _service.CreateAsync(command, new CancellationToken()));
    }
}
