using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers;

[Route("api/[controller]")]
public class CategoriaController : ControllerBase
{
    private readonly ICategoriaService _categoriaService;

    public CategoriaController(ICategoriaService categoriaService)
    {
        this._categoriaService = categoriaService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_categoriaService.Get());
    }

    [HttpPost]
    public IActionResult Post([FromBody] Categoria categoria)
    {
        _categoriaService.Save(categoria);
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult Put([FromBody] Categoria categoria, Guid id)
    {
        _categoriaService.Update(categoria, id);
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        _categoriaService.Delete(id);
        return Ok();
    }
}