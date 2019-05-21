namespace WeatherApi.Models
{
    public class WeatherApiModel
    {
        public LocationApiModel Location { get; set; }
        public CurrentApiModel Current { get; set; }
    }
}
