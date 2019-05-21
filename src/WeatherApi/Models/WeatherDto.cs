namespace WeatherApi.Models
{
    public class WeatherDto
    {
        public Location Location { get; set; }
        public Current Current { get; set; }  
        
        public Forecast Forecast { get; set; }
        
        
    }
}
