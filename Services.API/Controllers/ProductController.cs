using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Authen.Application.DTOs;

namespace Services.API.Controllers;

public class ProductController : BaseController
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [Authorize(Roles = "User")]
    [HttpGet("get-outsize")]
    //public async Task<ActionResult<AuthResponse>> getoutsize()
    //{
    //    ActionResult response = Unauthorized();
    //    response = Ok("asdas");
    //    return response;
    //}
    //[Authorize(Roles = "Admin")]
    //[HttpGet("get-role")]
    //public async Task<ActionResult<AuthResponse>> getrole()
    //{
    //    ActionResult response = Unauthorized();
    //    response = Ok("asdas");
    //    return response;
    //}

}
