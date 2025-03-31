using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.Authen.Application.Commands;
using Services.Authen.Application.DTOs;

namespace Services.API.Controllers;

public class AuthController : BaseController
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponse>> Register([FromBody] RegisterCommand command)
    {
        return await _mediator.Send(command);
    }
}
