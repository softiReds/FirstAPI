using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HelloWorlController : ControllerBase
{
    private readonly IHelloWorldService _helloWorldService;

    public HelloWorlController(IHelloWorldService helloWorldService)
    {
        this._helloWorldService = helloWorldService;
    }

    public IActionResult Get()
    {
        return Ok(_helloWorldService.GetHelloWorld());
    }
}