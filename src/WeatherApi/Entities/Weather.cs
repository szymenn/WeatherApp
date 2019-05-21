using System;

namespace WeatherApi.Entities
{
    public class Weather
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        
        public Guid UserId { get; set; }
    }
}
