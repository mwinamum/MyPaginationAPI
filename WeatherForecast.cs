using System;

namespace MyPaginationAPI
{
    public class WeatherForecast
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => TemperatureC * 9 / 5 + 32;

        public string Summary { get; set; }
    }
}
