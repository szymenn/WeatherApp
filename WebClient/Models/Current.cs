namespace WebClient.Models
{
    public class Current
    {
        public decimal TempC { get; set; }

        public decimal TempF { get; set; }

        public decimal Wind { get; set; }
        
        public Condition Condition { get; set; }
    }
}