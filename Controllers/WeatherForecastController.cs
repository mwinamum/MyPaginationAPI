using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MyPaginationAPI.Controllers
{
    public interface IWeatherForecastController
    {
        IActionResult Get(global::System.Int32 pageNumber = 1, global::System.Int32 pageSize = 2);
    }

    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase, IWeatherForecastController
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get(int pageNumber = 1, int pageSize = 2)
        {
            var rng = new Random();
            var allForecasts = Enumerable.Range(1, 2000).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }).ToList();

            // generate the below implementation as a class and to use it as such
            var paginator = new Paginator<WeatherForecast>(allForecasts, pageNumber, pageSize);
            var pagedForecasts = paginator.GetPagedResult();

            return Ok(pagedForecasts);
        }
    }
}

// Simple paginator class for demonstration
// This can be moved to a separate file for better organization
// Paginator.cs
// move this class to a separate file in the same namespace
namespace MyPaginationAPI
{
    public class Paginator<T>(IEnumerable<T> source, int pageNumber, int pageSize)
    {
        private readonly IEnumerable<T> _source = source;
        private readonly int _pageNumber = pageNumber;
        private readonly int _pageSize = pageSize;

        public List<T> GetPagedResult()
        {
            return _source
                .Skip((_pageNumber - 1) * _pageSize)
                .Take(_pageSize)
                .ToList();
        }
    }
}

