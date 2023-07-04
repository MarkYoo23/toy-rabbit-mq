using API.Applications.Services;
using API.Applications.Services.Users;
using API.Presentations.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Presentations.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] AddNewUserCommand request)
    {
        var user = await _mediator.Send(request);
        return Ok(user);
    }
}
