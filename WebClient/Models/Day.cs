namespace WebClient.Models
{
    public class Day
    {
        public decimal MaxTempC { get; set; }

        public decimal MaxTempF { get; set; }

        public decimal MinTempC { get; set; }

        public decimal MinTempF { get; set; }

        public decimal MaxWind { get; set; }

        public Condition Condition { get; set; }
    }
}