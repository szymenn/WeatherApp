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
                ReasonPhrase = "huj",
                Content = new StringContent(
                    "{\"location\": {\"name\": \"Warsaw\"}}"
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
                BaseAddress = new Uri("http://huj.com/")
            };
            
            var weatherApiClient = new WeatherApiClient(httpClient);
            var result = await weatherApiClient.GetWeatherDto("gowno");

            Assert.IsType<WeatherDto>(result);
        }

        [Fact]
        public async Task GetWeatherDto_WhenInvalidData_ThrowsException()
        {
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest,
                ReasonPhrase = "hehe",
                Content = new StringContent(
                    "{\"error\": {\"message\": \"spierdalajkurwa\"}}")
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
            var result = weatherApiClient.GetWeatherDto("invalid city");

            await Assert.ThrowsAsync<CityNotFoundException>(() => result);
        }

        [Fact]
        public async Task GetForecastDto_ByDefault_ReturnsCorrectType()
        {
            
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                ReasonPhrase = "huj",
                Content = new StringContent(
                    "{\"location\": {\"name\": \"Warsaw\"}}"
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
                BaseAddress = new Uri("http://huj.com/")
            };
            
            var weatherApiClient = new WeatherApiClient(httpClient);
            var result = await weatherApiClient.GetForecastDto("hioejrwijrwe");
            
            Assert.IsType<WeatherDto>(result);
        }

        [Fact]
        public async Task GetForecastDto_WhenInvalidData_ThrowsException()
        {
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest,
                ReasonPhrase = "hehe",
                Content = new StringContent(
                    "{\"error\": {\"message\": \"spierdalajkurwa\"}}")
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
            var result = weatherApiClient.GetForecastDto("invalid city");

            await Assert.ThrowsAsync<ForecastException>(() => result);
        }
    }
}