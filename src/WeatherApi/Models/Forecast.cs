using System.Collections.Generic;
using Newtonsoft.Json;

namespace WeatherApi.Models
{
    public class Forecast
    {
        [JsonProperty("forecastday")]
        public ICollection<ForecastDay> ForecastDays { get; set; }
    }
}