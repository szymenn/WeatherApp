
namespace WeatherApi.Exceptions
{
    public class CityNotFoundException : WeatherApiException
    {
        public CityNotFoundException()
        {
            
        }
        
        public CityNotFoundException(int statusCode, string reasonPhrase, string message)
            : base(statusCode, reasonPhrase, message)
        {
        }
    }
}