
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;
using WebClient.Exceptions;
using WebClient.Models;

namespace WebClient.Controllers
{
    [Route("[controller]")]
    public class ErrorController : Controller
    {
        [Route("{statusCode}")]
        public IActionResult HandleErrorCode(int statusCode)
        {
            var message = $"{statusCode}: ";
            var exceptionData = HttpContext.Features.Get<IExceptionHandlerFeature>();
            if (exceptionData.Error is WeatherApiException exception)
            {
                message = message + $"{exception.ProblemDetails.Title}. {exception.ProblemDetails.Detail}";
            }
            else
            {
                message = message + $"Internal Server Error.";
            }
            

            return View(new ErrorViewModel
            {
                Message = message
            });
        }
    }
}