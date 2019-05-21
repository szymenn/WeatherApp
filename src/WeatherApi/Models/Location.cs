using Newtonsoft.Json;

namespace WeatherApi.Models
{
    public class Location
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }
    }
}
