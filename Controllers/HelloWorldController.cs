using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HelloWorlController : ControllerBase
{
    private readonly IHelloWorldService _helloWorldService;

    private readonly ILogger<HelloWorlController> _logger;

    public HelloWorlController(IHelloWorldService helloWorldService, ILogger<HelloWorlController> logger)
    {
        this._logger = logger;
        this._helloWorldService = helloWorldService;
    }

    public IActionResult Get()
    {
        _logger.LogInformation("Mostrando la informacion desde la dependencia");
        return Ok(_helloWorldService.GetHelloWorld());
    }
}