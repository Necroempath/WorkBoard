using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkBoard.Application.Features.Auth;
using WorkBoard.Application.Features.DTOs;

namespace WorkBoard.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponseDto>> Register([FromBody]RegisterRequest request, CancellationToken token)
    {
        return Ok(await _mediator.Send(new RegisterCommand(request), token));
    }
}
