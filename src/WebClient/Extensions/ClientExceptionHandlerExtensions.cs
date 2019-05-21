using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using WebClient.Exceptions;

namespace WebClient.Extensions
 {
     public static class ClientExceptionHandlerExtensions
     {
         public static void UseCustomExceptionHandler(this IApplicationBuilder app)
         {
             app.UseExceptionHandler(errorApp =>
             {
                 errorApp.Run(async context =>
                 {
                     var errorFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                     if (errorFeature.Error is WeatherApiException exception)
                     {
                         if (exception.ProblemDetails.Status != null)
                         {
                             context.Response.StatusCode = exception.ProblemDetails.Status.Value;
                         }
                     }
                     else
                     {
                         context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                     }
                 });
             });
         }
     }
 }