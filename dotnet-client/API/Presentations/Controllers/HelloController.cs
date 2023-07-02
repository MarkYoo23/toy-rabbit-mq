using API.Applications.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Presentations.Controllers;

[ApiController]
[Route("[controller]")]
public class HelloController : ControllerBase
{
    [HttpPost]
    public IActionResult SendMessage([FromServices] SayHelloService service)
    {
        service.Execute();
        return Ok();
    }
}
