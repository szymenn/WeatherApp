namespace WeatherApi.Models
{
    public class WeatherForecastApiModel
    {
        public LocationApiModel Location { get; set; }
        
        public CurrentApiModel Current { get; set; }  
        
        public ForecastApiModel Forecast { get; set; }
    }
}