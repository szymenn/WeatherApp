using System;

namespace WeatherApi.Exceptions
{
    public class WeatherApiException : ApplicationException
    {
        public int StatusCode { get; set; }
        public override string Message { get; }

        public string ReasonPhrase { get; set; }

        public WeatherApiException()
        {

        }

        protected WeatherApiException(int statusCode, string reasonPhrase, string message)
        {
            StatusCode = statusCode;
            Message = message;
            ReasonPhrase = reasonPhrase;
        }
    }
}