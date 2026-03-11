using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkBoard.Application.Features.Authentication;
using WorkBoard.Application.Features.Authentication.Commands;

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

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login([FromBody]LoginRequest request, CancellationToken token)
    {
        return Ok(await _mediator.Send(new LoginCommand(request), token));
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<AuthResponseDto>> Refresh([FromBody]RefreshTokenRequest request, CancellationToken token)
    {
        return Ok(await _mediator.Send(new RefreshTokenCommand(request), token));
    }
}