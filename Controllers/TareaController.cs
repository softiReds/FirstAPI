using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers;

[Route("api/[controller]")]
public class TareaController : ControllerBase
{
    private readonly ITareaService _tareaService;

    public TareaController(ITareaService tareaService)
    {
        this._tareaService = tareaService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_tareaService.Get());
    }

    [HttpPost]
    public IActionResult Post([FromBody] Tarea tarea)
    {
        _tareaService.Save(tarea);
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult Put([FromBody] Tarea tarea, Guid id)
    {
        _tareaService.Update(tarea, id);
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        _tareaService.Delete(id);
        return Ok();
    }
}