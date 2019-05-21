using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherApi.Exceptions;

namespace WeatherApi.Extensions
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var problemDetails = new ProblemDetails()
            {
                Instance = $"urn:myweatherapp:{Guid.NewGuid()}"
            };
            try
            {
                await _next(context);
            }
            catch (CityAlreadyAssignedException cityAssignedEx)
            {
                problemDetails.Title = cityAssignedEx.ReasonPhrase;
                problemDetails.Status = cityAssignedEx.StatusCode;
                problemDetails.Detail = cityAssignedEx.Message;
                context.Response.StatusCode = problemDetails.Status.Value;
                context.Response.WriteJson(problemDetails);
            }
            catch (ForecastException forecastEx)
            {
                problemDetails.Title = forecastEx.ReasonPhrase;
                problemDetails.Status = forecastEx.StatusCode;
                problemDetails.Detail = forecastEx.Message;
                context.Response.StatusCode = problemDetails.Status.Value;
                context.Response.WriteJson(problemDetails);
            }
            catch (CityNotFoundException cityNotFoundEx)
            {
                problemDetails.Title = cityNotFoundEx.ReasonPhrase;
                problemDetails.Status = cityNotFoundEx.StatusCode;
                problemDetails.Detail = cityNotFoundEx.Message;
                context.Response.StatusCode = problemDetails.Status.Value;
                context.Response.WriteJson(problemDetails);
            }
            catch (CityNotAssignedException cityNotAssignedEx)
            {
                problemDetails.Title = cityNotAssignedEx.ReasonPhrase;
                problemDetails.Status = cityNotAssignedEx.StatusCode;
                problemDetails.Detail = cityNotAssignedEx.Message;
                context.Response.StatusCode = problemDetails.Status.Value;
                context.Response.WriteJson(problemDetails);
            }
            catch (Exception ex)
            {
                problemDetails.Title = "Internal Server Error";
                problemDetails.Status = StatusCodes.Status500InternalServerError;
                problemDetails.Detail = "An unexcepted error occured";
            }
        }
    }
}