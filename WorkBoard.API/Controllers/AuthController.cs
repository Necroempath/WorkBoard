using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkBoard.API.Services;
using WorkBoard.Application.Features.Authentication;
using WorkBoard.Application.Features.Authentication.Commands;

namespace WorkBoard.API.Controllers;

[Route("auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ICookieService _cookieService;

    public AuthController(IMediator mediator, ICookieService cookieService)
    {
        _mediator = mediator;
        _cookieService = cookieService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponseDto>> Register([FromBody]RegisterRequest request, CancellationToken token)
    {
        var authResponseDto = await _mediator.Send(new RegisterCommand(request), token);
        
        _cookieService.SetRefreshToken(Response, authResponseDto.RefreshToken);

        return Ok(authResponseDto);
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login([FromBody]LoginRequest request, CancellationToken token)
    {
        var authResponseDto = await _mediator.Send(new LoginCommand(request), token);

        _cookieService.SetRefreshToken(Response, authResponseDto.RefreshToken);

        return Ok(authResponseDto);
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<string>> Refresh(CancellationToken token)
    {
        var refreshToken = _cookieService.TryGet(Request);

        if (refreshToken is null)
            return Unauthorized();

        var tokens = await _mediator.Send(new RefreshTokenCommand(refreshToken), token);

        _cookieService.SetRefreshToken(Response, tokens.RefreshToken);

        return Ok(tokens.AccessToken);
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        _cookieService.DeleteRefreshToken(Response);

        return NoContent();
    }
}