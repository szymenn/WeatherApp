using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using WeatherApi.Data;
using WeatherApi.Entities;
using WeatherApi.Exceptions;
using WeatherApi.Models;
using WeatherApi.Services;
using Xunit;

namespace WeatherApi.Tests
{
    public class WeatherServiceTests : WeatherServiceTestBase
    {
        [Fact]
        public async Task GetWeather_ByDefault_ReturnsCorrectType()
        {
            var httpClientStub = new Mock<IWeatherApiClient>();
            var appDbContextStub = new Mock<IDbContext>();
            var mapperStub = new Mock<IMapper>(); 
            httpClientStub.Setup(e => e.GetWeatherDto("Warsaw"))
                .Returns(Task.FromResult(new WeatherDto()));
            
            var weatherService = new WeatherService
                (mapperStub.Object, appDbContextStub.Object, httpClientStub.Object);
            var actual = await weatherService.GetWeather("Warsaw");
            
            Assert.IsType<WeatherDto>(actual);
        }
        
        [Fact]
        public async Task SaveWeather_ByDefault_SavesCorrectly()
        {
            var httpClientStub = new Mock<IWeatherApiClient>();
            var mapperStub = new Mock<IMapper>();
            httpClientStub.Setup(e => e.GetWeatherDto("Warsaw"))
                .Returns(Task.FromResult(new WeatherDto()));
            mapperStub.Setup(e => e.Map<Weather>(It.IsAny<WeatherDto>()))
                .Returns(new Weather());

            var weatherService = new WeatherService
                (mapperStub.Object, Context, httpClientStub.Object);
            var actual = await weatherService.SaveWeather("Warsaw", Guid.NewGuid());

            Assert.IsType<WeatherDto>(actual);
        }

        [Fact]
        public async Task GetUserWeather_ByDefault_ReturnsCorrectType()
        {
            var httpClientStub = new Mock<IWeatherApiClient>();
            var mapperStub = new Mock<IMapper>();
            httpClientStub.Setup(e => e.GetWeatherDto(It.IsAny<string>()))
                .Returns(Task.FromResult(new WeatherDto()));
            
            var weatherService = new WeatherService
                (mapperStub.Object, Context, httpClientStub.Object);
            var actual = await weatherService.GetUserWeather(Guid.NewGuid());

            Assert.IsAssignableFrom<ICollection<WeatherDto>>(actual);
        }

        [Fact]
        public async Task GetForecast_ByDefault_ReturnsCorrectType()
        {
            var httpClientStub = new Mock<IWeatherApiClient>();
            var mapperStub = new Mock<IMapper>();
            httpClientStub.Setup(e => e.GetForecastDto(It.IsAny<string>()))
                .Returns(Task.FromResult(new WeatherDto()));
            
            var weatherService = new WeatherService
                (mapperStub.Object, Context, httpClientStub.Object);
            var actual = await weatherService.GetForecast(It.IsAny<string>());

            Assert.IsType<WeatherDto>(actual);
        }
        
        [Fact]
        public async Task SaveWeather_WhenCityAssigned_ThrowsException()
        {
            var httpClientStub = new Mock<IWeatherApiClient>();
            var mapperStub = new Mock<IMapper>();
            httpClientStub.Setup(e => e.GetWeatherDto("Warsaw"))
                .Returns(Task.FromResult(new WeatherDto()));
            
            var weatherService = new WeatherService
                (mapperStub.Object, Context, httpClientStub.Object);

            var actual = weatherService.SaveWeather
                ("Warsaw", Guid.Parse("31035f07-4524-4adc-b0cf-1a53d4eb3fb1"));
            
            await Assert.ThrowsAsync<CityAlreadyAssignedException>(() => actual);
        }

        [Fact]
        public async Task DeleteWeather_ByDefault_ReturnsCorrectType()
        {
            var httpClientStub = new Mock<IWeatherApiClient>();
            var mapperStub = new Mock<IMapper>();
            httpClientStub.Setup(e => e.GetWeatherDto("Warsaw"))
                .Returns(Task.FromResult(new WeatherDto()));
            
            var weatherService = new WeatherService
                (mapperStub.Object, Context, httpClientStub.Object);

            var actual = await weatherService.DeleteWeather
                    ("Warsaw", Guid.Parse("31035f07-4524-4adc-b0cf-1a53d4eb3fb1"));

            Assert.IsAssignableFrom<ICollection<WeatherDto>>(actual);
        }

        [Fact]
        public async Task DeleteWeather_WhenCityNotAssigned_ThrowsException()
        {
            var httpClientStub = new Mock<IWeatherApiClient>();
            var mapperStub = new Mock<IMapper>();
            httpClientStub.Setup(e => e.GetWeatherDto("Warsaw"))
                .Returns(Task.FromResult(new WeatherDto()));

            var weatherService = new WeatherService
                (mapperStub.Object, Context, httpClientStub.Object);
            var actual = weatherService.DeleteWeather
                ("Warsaw", Guid.Parse("46b41624-0050-4221-a722-f06914f3f152"));

            await Assert.ThrowsAsync<CityNotAssignedException>(() => actual);
        }
    }

}