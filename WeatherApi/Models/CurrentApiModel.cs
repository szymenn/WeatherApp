namespace WeatherApi.Models
{
    public class CurrentApiModel
    {
        public decimal TempC { get; set; }

        public decimal TempF { get; set; }

        public decimal Wind { get; set; }
        
        public ConditionApiModel Condition { get; set; }
    }
}
