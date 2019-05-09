namespace WeatherApi.Exceptions
{
    public class CityAlreadyAssignedException : WeatherApiException
    {
        public CityAlreadyAssignedException()
        {
            
        }
        
        public CityAlreadyAssignedException(int statusCode, string reasonPhrase,string message)
            :base(statusCode, reasonPhrase, message)
        {
            
        }
    }
}