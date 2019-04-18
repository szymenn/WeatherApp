using Newtonsoft.Json;

namespace WeatherApi.Models
{
    public class Day
    {
        [JsonProperty("maxtemp_c")]
        public decimal MaxTempC { get; set; }
        
        [JsonProperty("maxtemp_f")]
        public decimal MaxTempF { get; set; }
        
        [JsonProperty("mintemp_c")]
        public decimal MinTempC { get; set; }
        
        [JsonProperty("mintemp_f")]
        public decimal MinTempF { get; set; }
        
        [JsonProperty("maxwind_kph")]
        public decimal MaxWind { get; set; }
        
        [JsonProperty("condition")]
        public Condition Condition { get; set; }
        
    }
}