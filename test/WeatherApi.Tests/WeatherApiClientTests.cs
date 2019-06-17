using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using WeatherApi.Exceptions;
using WeatherApi.Models;
using WeatherApi.Services;
using Xunit;

namespace WeatherApi.Tests
{
    public class WeatherApiClientTests
    {
        [Fact]
        public async Task GetWeatherDto_ByDefault_ReturnsCorrectType()
        {
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                ReasonPhrase = It.IsAny<string>(),
                Content = new StringContent(
                    "{\"location\": {\"name\": \"city\"}}"
                )
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>
                (
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(response)
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://something.com/")
            };
            
            var weatherApiClient = new WeatherApiClient(httpClient);
            var result = await weatherApiClient.GetWeatherDto(It.IsAny<string>());

            Assert.IsType<WeatherDto>(result);
        }

        [Fact]
        public async Task GetWeatherDto_WhenInvalidData_ThrowsException()
        {
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest,
                ReasonPhrase = It.IsAny<string>(),
                Content = new StringContent(
                    "{\"error\": {\"message\": \"error\"}}")
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>
                (
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(response)
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://something.com/")
            };
            
            var weatherApiClient = new WeatherApiClient(httpClient);
            var result = weatherApiClient.GetWeatherDto(It.IsAny<string>());

            await Assert.ThrowsAsync<CityNotFoundException>(() => result);
        }

        [Fact]
        public async Task GetForecastDto_ByDefault_ReturnsCorrectType()
        {
            
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                ReasonPhrase = It.IsAny<string>(),
                Content = new StringContent(
                    "{\"location\": {\"name\": \"city\"}}"
                )
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>
                (
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(response)
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://something.com/")
            };
            
            var weatherApiClient = new WeatherApiClient(httpClient);
            var result = await weatherApiClient.GetForecastDto(It.IsAny<string>());
            
            Assert.IsType<WeatherDto>(result);
        }

        [Fact]
        public async Task GetForecastDto_WhenInvalidData_ThrowsException()
        {
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest,
                ReasonPhrase = It.IsAny<string>(),
                Content = new StringContent(
                    "{\"error\": {\"message\": \"message\"}}")
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>
                (
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(response)
                .Verifiable();

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://something.com/")
            };
            
            var weatherApiClient = new WeatherApiClient(httpClient);
            var result = weatherApiClient.GetForecastDto(It.IsAny<string>());

            await Assert.ThrowsAsync<ForecastException>(() => result);
        }
    }
}