using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyPaginationAPI;

namespace MyPaginationAPI.Controllers
{
    /// <summary>
    /// Controller for handling weather forecast requests with pagination support.
    /// </summary>
    /// <remarks>
    /// This controller provides an endpoint to retrieve weather forecast data in a paginated format.
    /// The <see cref="Get"/> method allows clients to specify the page number and page size for the results.
    /// </remarks>
    public interface IWeatherForecastController
    {
        IActionResult Get(int pageNumber = 1, int pageSize = 2);
    }

    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase, IWeatherForecastController
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private static readonly List<WeatherForecast> _forecasts;
        private static int _nextId = 1;

        static WeatherForecastController()
        {
            var rng = new Random();
            _forecasts = Enumerable.Range(1, 2000).Select(index => new WeatherForecast
            {
                Id = _nextId++,
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }).ToList();
        }

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get(int pageNumber = 1, int pageSize = 2)
        {
            var paginator = new Paginator<WeatherForecast>(_forecasts, pageNumber, pageSize);
            var pagedForecasts = paginator.GetPagedResult();
            return Ok(pagedForecasts);
        }

        /// <summary>
        /// Adds a new weather forecast entry.
        /// </summary>
        [HttpPost]
        public IActionResult Add([FromBody] WeatherForecast forecast)
        {
            if (forecast == null)
                return BadRequest("Forecast cannot be null.");

            forecast.Id = _nextId++;
            _forecasts.Add(forecast);
            return CreatedAtAction(nameof(Get), new { }, forecast);
        }

        /// <summary>
        /// Updates an existing weather forecast entry.
        /// </summary>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] WeatherForecast forecast)
        {
            if (forecast == null)
                return BadRequest("Forecast cannot be null.");

            var existing = _forecasts.FirstOrDefault(f => f.Id == id);
            if (existing == null)
                return NotFound();

            // Update properties
            existing.Date = forecast.Date;
            existing.TemperatureC = forecast.TemperatureC;
            existing.Summary = forecast.Summary;

            return Ok(existing);
        }
    }
}
