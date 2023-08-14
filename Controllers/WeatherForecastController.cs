// Los controladores son clases que manejan los request dentro de la API, todos los modelos del proyecto deben estár relacionados con un controlador (cada modelo tiene su propio controlador)

using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")] // La ruta especifica como se debe consumir el controlador, en este caso, el nombre del controlador será la forma mediante la cual accederemos a los endpoints del controlador
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    private static List<WeatherForecast> ListWeatherForecast = new List<WeatherForecast>();

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;

        if (ListWeatherForecast == null || !ListWeatherForecast.Any())
        {
            // Any() -> Verifica si la lista tiene algun registro

            ListWeatherForecast = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }).ToList();
        }
    }

    /*

    [HttpVerb]
    public dataType FunctionName()
    {
        return value;
    }

    */

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return ListWeatherForecast;
    }

    [HttpPost]
    public IActionResult Post(WeatherForecast weatherForecast)
    {
        // IActionResult -> Tipo de dato utilizado para los retornos en REST
        ListWeatherForecast.Add(weatherForecast);

        return Ok();
    }

    [HttpDelete("{index}")] // {elementFromUrl} -> Especificamos que dentro de la URL vendrá algun elemento que utilizaremos en el codigo
    public IActionResult Delete(int elementIndex)
    {
        ListWeatherForecast.RemoveAt(elementIndex);

        return Ok();
    }
}
