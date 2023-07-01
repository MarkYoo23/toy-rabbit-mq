using API.Applications.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Presentations.Controllers;

[ApiController]
[Route("[controller]")]
public class HelloController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> SendMessage([FromServices] SayHelloService service)
    {
        await service.ExecuteAsync();
        return Ok();
    }
}
