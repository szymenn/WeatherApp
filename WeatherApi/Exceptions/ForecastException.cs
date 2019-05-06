
namespace WeatherApi.Exceptions
{
    public class ForecastException : WeatherApiException
    {
        public ForecastException() : base()
        {
            
        }
        
        public ForecastException(int statusCode, string reasonPhrase, string message)
            : base(statusCode, reasonPhrase, message)
        {
            
        }
    }
}