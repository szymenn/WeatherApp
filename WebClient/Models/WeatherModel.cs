using System.Collections.Generic;

namespace WebClient.Models
{
    public class WeatherModel
    {
        public ICollection<WeatherViewModel> WeatherViewModels { get; set; }
        
        public string City { get; set; }
    }
}