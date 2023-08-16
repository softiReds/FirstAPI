// Los controladores son clases que manejan los request dentro de la API, todos los modelos del proyecto deben estár relacionados con un controlador (cada modelo tiene su propio controlador)

using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]     // Enrutamiento a nivel del controlador
// La ruta especifica como se debe consumir el controlador, en este caso, el nombre del controlador será la forma mediante la cual accederemos a los endpoints del controlador
// "[controller]" -> Permite que se maneje una ruta dinamica, en este caso se maneja el nombre del controladol Sin necesidad de especificar el nombre del controlador, podremos acceder al controlador mediante el nombre (independientemente de los cambios que se hhagan en el nombre, la ruta dinamica toma los cambios automaticamente)
// "[parameter]" -> Parametro dinamico

public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;    // Loggin del controlador -> Se utiliza para registrar informacion, advertencias y errores en tiempo de ejecucion

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
    // [Route("Get/weatherforecast")]  // Ruta a nivel de request
    // La ruta para aceder a este metodo en especifico debemos hacer uso de la ruta a nivel de controlador y la ruta a nivel del request (metodo) -> servidor/api/[controller]/Get/weatherforecast
    // [Route("Get/weatherforecast2")] // Podemos asignar mas de una ruta por request, el metodo se ejecutará con cualquiera de las rutas configuradas
    // [Route("[action]")] // [action] -> Permite que se utilice el nombre del metodo para hacer el llamado del endpoint
    // [Route("[action]/weatherforecast")]  -> Ruta similar a la que se encuentra en la linea 51, utiliza el nombre dle metodo ("Get") y otra palabra ("weatherforecast")
    public IEnumerable<WeatherForecast> Get()
    {
        _logger.LogDebug("Retornando la lista de weatherForecast");
        // LogLevel("message") -> Muestra el mensaje en la consola
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
