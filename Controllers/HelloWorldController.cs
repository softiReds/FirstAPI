using Microsoft.AspNetCore.Mvc;
using webapi;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HelloWorlController : ControllerBase
{
    TareasContext dbContext;

    private readonly IHelloWorldService _helloWorldService;

    private readonly ILogger<HelloWorlController> _logger;

    public HelloWorlController(IHelloWorldService helloWorldService, ILogger<HelloWorlController> logger, TareasContext context)
    {
        this._logger = logger;
        this._helloWorldService = helloWorldService;
        this.dbContext = context;
    }

    [HttpGet]
    public IActionResult Get()
    {
        _logger.LogInformation("Mostrando la informacion desde la dependencia");
        return Ok(_helloWorldService.GetHelloWorld());
    }

    [HttpGet]
    [Route("createdb")]
    public IActionResult CreateDataBase()
    {
        dbContext.Database.EnsureCreated();
        return Ok();
    }
}