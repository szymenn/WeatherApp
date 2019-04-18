using System.Collections.Generic;

namespace WeatherApi.Models
{
    public class ForecastApiModel
    {
        public ICollection<ForecastDayApiModel> ForecastDays { get; set; }
    }
}