using System.Collections.Generic;

namespace WebClient.Models
{
    public class Forecast
    {
        public ICollection<ForecastDay> ForecastDays { get; set; }
    }
}