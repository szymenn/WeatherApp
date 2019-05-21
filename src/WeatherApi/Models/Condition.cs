using Newtonsoft.Json;

namespace WeatherApi.Models
{
    public class Condition
    {
        [JsonProperty("icon")]
        public string Icon { get; set; }
    }
}