namespace WeatherApi.Exceptions
{
    public class CityNotAssignedException : WeatherApiException
    {
        public CityNotAssignedException(int statusCode, string reasonPhrase, string message) 
            : base(statusCode, reasonPhrase, message)
        {
            
        }        
    }
}