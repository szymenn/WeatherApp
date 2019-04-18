using System.Security.Cryptography.X509Certificates;

namespace WebClient.Models
{
    public class WeatherDto
    {
        public Location Location { get; set; }
        public Current Current { get; set; }   
        
        public Forecast Forecast { get; set; }
        
        public int StatusCode { get; set; }
        
    }
}