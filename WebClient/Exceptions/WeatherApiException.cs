using System;
using Microsoft.AspNetCore.Mvc;

namespace WebClient.Exceptions
{
    public class WeatherApiException : ApplicationException
    {
        public ProblemDetails ProblemDetails { get; set; }

        public WeatherApiException(ProblemDetails problemDetails)
        {
            ProblemDetails = problemDetails;
        }
    }
}