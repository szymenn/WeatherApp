using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherApi.Exceptions;

namespace WeatherApi.Extensions
{
    public static class ApiExceptionHandlerExtensions
    {
        public static void UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var problemDetails = new ProblemDetails()
                    {
                        Instance = $"urn:myweatherapp:{Guid.NewGuid()}"
                    };
                    switch (errorFeature.Error)
                    {
                        case CityAlreadyAssignedException assignedException:
                            problemDetails.Title = assignedException.ReasonPhrase;
                            problemDetails.Status = assignedException.StatusCode;
                            problemDetails.Detail = assignedException.Message;
                            break;
                        case CityNotFoundException notFoundException:
                            problemDetails.Title = notFoundException.ReasonPhrase;
                            problemDetails.Status = notFoundException.StatusCode;
                            problemDetails.Detail = notFoundException.Message;
                            break;
                        case CityNotAssignedException notAssignedException:
                            problemDetails.Title = notAssignedException.ReasonPhrase;
                            problemDetails.Status = notAssignedException.StatusCode;
                            problemDetails.Detail = notAssignedException.Message;
                            break;
                        case ForecastException forecastException:
                            problemDetails.Title = forecastException.ReasonPhrase;
                            problemDetails.Status = forecastException.StatusCode;
                            problemDetails.Detail = forecastException.Message;
                            break;
                        default:
                            problemDetails.Title = "Internal server error.";
                            problemDetails.Status = StatusCodes.Status500InternalServerError;
                            problemDetails.Detail = "An unexpected error occured.";
                            break;
                    }

                    
                    context.Response.StatusCode = problemDetails.Status.Value;
                    context.Response.WriteJson(problemDetails);
                });
            });
        }
    }

}