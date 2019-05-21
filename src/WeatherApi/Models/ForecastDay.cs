using System;
using Newtonsoft.Json;

namespace WeatherApi.Models
{
    public class ForecastDay
    {
        [JsonProperty("date")]
        public DateTimeOffset Date { get; set; }
        
        [JsonProperty("day")]
        public Day Day { get; set; }
    }
}