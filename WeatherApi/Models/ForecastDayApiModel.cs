using System;

namespace WeatherApi.Models
{
    public class ForecastDayApiModel
    {
        public DateTimeOffset Date { get; set; }
        
        public DayApiModel Day { get; set; }
    }
}