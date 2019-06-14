using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using WeatherApi.Exceptions;
using WeatherApi.Extensions;
using Xunit;

namespace WeatherApi.Tests
{
    public class CustomExceptionHandlerMiddlewareTests
    {
        [Fact]
        public async Task Invoke_WhenCityNotFound_SetsBadRequestStatusCode()
        {
            var middleware = new CustomExceptionHandlerMiddleware
            (context => throw new CityNotFoundException(StatusCodes.Status400BadRequest,
                It.IsAny<string>(),
                It.IsAny<string>()));

            var httpContext = new DefaultHttpContext();

            await middleware.Invoke(httpContext);

            Assert.Equal(StatusCodes.Status400BadRequest,httpContext.Response.StatusCode);
        }

        [Fact]
        public async Task Invoke_WhenCityNotFound_WritesProblemDetails()
        {
            var middleware = new CustomExceptionHandlerMiddleware(
                context => throw new CityNotFoundException(It.IsAny<int>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()));

            var httpContext = new DefaultHttpContext();
            httpContext.Response.Body = new MemoryStream();

            await middleware.Invoke(httpContext);

            httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
            var jsonString = new StreamReader(httpContext.Response.Body).ReadToEnd();
            var actual = JsonConvert.DeserializeObject<ProblemDetails>(jsonString);

            Assert.IsType<ProblemDetails>(actual);
        }

        [Fact]
        public async Task Invoke_WhenCityAssigned_SetsConflictStatusCode()
        {
            var middleware = new CustomExceptionHandlerMiddleware(
                context => throw new CityAlreadyAssignedException(StatusCodes.Status409Conflict,
                    It.IsAny<string>(),
                    It.IsAny<string>()));
            
            var httpContext = new DefaultHttpContext();

            await middleware.Invoke(httpContext);
            
            Assert.Equal(StatusCodes.Status409Conflict, httpContext.Response.StatusCode);
        }

        [Fact]
        public async Task Invoke_WhenCityAssigned_WritesProblemDetails()
        {
            var middleware = new CustomExceptionHandlerMiddleware(
                context => throw new CityAlreadyAssignedException(It.IsAny<int>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()));
            
            var httpContext = new DefaultHttpContext();
            httpContext.Response.Body = new MemoryStream();

            await middleware.Invoke(httpContext);

            httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
            var jsonString = new StreamReader(httpContext.Response.Body).ReadToEnd();
            var actual = JsonConvert.DeserializeObject<ProblemDetails>(jsonString);

            Assert.IsType<ProblemDetails>(actual);
        }

        [Fact]
        public async Task Invoke_WhenCityNotAssigned_SetsNotFoundStatusCode()
        {
            var middleware = new CustomExceptionHandlerMiddleware(
                context => throw new CityNotAssignedException(StatusCodes.Status404NotFound,
                    It.IsAny<string>(),
                    It.IsAny<string>()));
            
            var httpContext = new DefaultHttpContext();

            await middleware.Invoke(httpContext);
            
            Assert.Equal(StatusCodes.Status404NotFound, httpContext.Response.StatusCode);
        }

        [Fact]
        public async Task Invoke_WhenCityNotAssigned_WritesProblemDetails()
        {
            var middleware = new CustomExceptionHandlerMiddleware(
                context => throw new CityNotAssignedException(It.IsAny<int>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()));
            
            var httpContext = new DefaultHttpContext();
            httpContext.Response.Body = new MemoryStream();

            await middleware.Invoke(httpContext);

            httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
            var jsonString = new StreamReader(httpContext.Response.Body).ReadToEnd();
            var actual = JsonConvert.DeserializeObject<ProblemDetails>(jsonString);

            Assert.IsType<ProblemDetails>(actual);
        }

        [Fact]
        public async Task Invoke_WhenForecastNotFound_SetsBadRequestStatusCode()
        {
            var middleware = new CustomExceptionHandlerMiddleware(
                context => throw new ForecastException(StatusCodes.Status400BadRequest,
                    It.IsAny<string>(),
                    It.IsAny<string>()));
            
            var httpContext = new DefaultHttpContext();

            await middleware.Invoke(httpContext);

            Assert.Equal(StatusCodes.Status400BadRequest, httpContext.Response.StatusCode);
        }

        [Fact]
        public async Task Invoke_WhenForecastNotFound_WritesProblemDetails()
        {
            var middleware = new CustomExceptionHandlerMiddleware(
                context => throw new ForecastException(It.IsAny<int>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()));
            
            var httpContext = new DefaultHttpContext();
            httpContext.Response.Body = new MemoryStream();

            await middleware.Invoke(httpContext);

            httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
            var jsonString = new StreamReader(httpContext.Response.Body).ReadToEnd();
            var actual = JsonConvert.DeserializeObject<ProblemDetails>(jsonString);

            Assert.IsType<ProblemDetails>(actual);
        }

        [Fact]
        public async Task Invoke_WhenException_SetsInternalServerErrorStatusCode()
        {
            var middleware = new CustomExceptionHandlerMiddleware(
                context => throw new Exception());

            var httpContext = new DefaultHttpContext();

            await middleware.Invoke(httpContext);
            
            Assert.Equal(StatusCodes.Status500InternalServerError, httpContext.Response.StatusCode);
        }

        [Fact]
        public async Task Invoke_WhenException_WritesJson()
        {
            var middleware = new CustomExceptionHandlerMiddleware(
                context => throw new Exception());
            
            var httpContext = new DefaultHttpContext();
            httpContext.Response.Body = new MemoryStream();

            await middleware.Invoke(httpContext);

            httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
            var jsonString = new StreamReader(httpContext.Response.Body).ReadToEnd();
            var actual = JsonConvert.DeserializeObject<ProblemDetails>(jsonString);

            Assert.IsType<ProblemDetails>(actual);
        }
    }
}