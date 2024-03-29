using dotnet7_new_features.IParseable;
using Microsoft.AspNetCore.Mvc;

namespace dotnet7_new_features.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        //call this endpoint like: WeatherForecast/1-4
        [HttpGet("{days}", Name = "GetWithParsable")]
        public IEnumerable<WeatherForecast> GetWithParsable(Days days)
        {
            return Enumerable.Range(days.From, days.To).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        //[FromServices] no longer necessary
        [HttpGet("GetSomething")]
        public ActionResult GetSomething() => Ok();

        // This will return exactly what we wait, in .net6 it was serializing whole result object
        [HttpGet("IResultSupported")]
        public IResult TestIResultSupport()
        {
            return Results.Ok(new { name = "Cihan", surname = "Tekin" });
        }
    }
}