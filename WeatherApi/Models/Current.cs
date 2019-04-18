using Newtonsoft.Json;

namespace WeatherApi.Models
{
    public class Current
    {
        [JsonProperty("temp_c")]
        public decimal TempC { get; set; }

        [JsonProperty("temp_f")]
        public decimal TempF { get; set; }

        [JsonProperty("wind_kph")]
        public decimal Wind { get; set; }
        
        public Condition Condition { get; set; }
    }
}
