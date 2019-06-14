using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using IdentityModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WeatherApi.Controllers;
using WeatherApi.Exceptions;
using WeatherApi.Models;
using WeatherApi.Services;
using Xunit;

namespace WeatherApi.Tests
{
    public class WeatherControllerTests
    {
        [Fact]
        public async Task GetWeather_ByDefault_ReturnsCorrectType()
        {
            var weatherServiceStub = new Mock<IWeatherService>();
            var mapperServiceStub = new Mock<IMapper>();
            weatherServiceStub.Setup(e => e.GetWeather(It.IsAny<string>()))
                .Returns(Task.FromResult(new WeatherDto()));

            var weatherController = new WeatherController
                (weatherServiceStub.Object, mapperServiceStub.Object);
            var actual = await weatherController.GetWeather(It.IsAny<string>());

            Assert.IsAssignableFrom<OkObjectResult>(actual);
        }

        [Fact]
        public async Task GetWeather_WhenInvalidCity_ThrowsException()
        {
            var weatherServiceStub = new Mock<IWeatherService>();
            var mapperServiceStub = new Mock<IMapper>();
            weatherServiceStub.Setup(e => e.GetWeather(It.IsAny<string>()))
                .Returns(Task.FromResult(new WeatherDto()));
            weatherServiceStub.Setup(e => e.GetWeather(It.Is<string>(m => m != "Warsaw")))
                .Throws<CityNotFoundException>();

            var weatherController = new WeatherController
                (weatherServiceStub.Object, mapperServiceStub.Object);
            var actual = weatherController.GetWeather(It.Is<string>(m => m != "Warsaw"));

            await Assert.ThrowsAsync<CityNotFoundException>(() => actual);
        }

        [Fact]
        public async Task GetForecast_ByDefault_ReturnsOkObjectResult()
        {
            var weatherServiceStub = new Mock<IWeatherService>();
            var mapperServiceStub = new Mock<IMapper>();
            weatherServiceStub.Setup(e => e.GetForecast(It.IsAny<string>()))
                .Returns(Task.FromResult(new WeatherDto()));

            var weatherController = new WeatherController
                (weatherServiceStub.Object, mapperServiceStub.Object);
            var actual = await weatherController.GetForecast(It.IsAny<string>());

            Assert.IsAssignableFrom<OkObjectResult>(actual);
        }

        [Fact]
        public async Task GetForecast_WhenInvalidCity_ThrowsException()
        {
            var weatherServiceStub = new Mock<IWeatherService>();
            var mapperServiceStub = new Mock<IMapper>();
            weatherServiceStub.Setup(e => e.GetForecast(It.IsAny<string>()))
                .Returns(Task.FromResult(new WeatherDto()));
            weatherServiceStub.Setup(e => e.GetForecast(It.Is<string>(m => m != "Warsaw")))
                .Throws<ForecastException>();

            var weatherController = new WeatherController
                (weatherServiceStub.Object, mapperServiceStub.Object);
            var actual = weatherController.GetForecast(It.Is<string>(m => m != "Warsaw"));

            await Assert.ThrowsAsync<ForecastException>(() => actual);
        }
        
        [Fact]
        public async Task GetUserWeather_ByDefault_ReturnsCorrectType()
        {
            var mapperStub = new Mock<IMapper>();
            var weatherServiceStub = new Mock<IWeatherService>();
            
            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(JwtClaimTypes.Subject, Guid.NewGuid().ToString()), 
            }));

            var controller = new WeatherController
                (weatherServiceStub.Object, mapperStub.Object)
                {
                    ControllerContext = new ControllerContext 
                        {HttpContext = new DefaultHttpContext {User = user}}
                };

            var result = await controller.GetUserWeather();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SaveCity_ByDefault_ReturnsCorrectType()
        {
            var mapperStub = new Mock<IMapper>();
            var weatherServiceStub = new Mock<IWeatherService>();
            
            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(JwtClaimTypes.Subject, Guid.NewGuid().ToString()), 
            }));

            var controller = new WeatherController
                (weatherServiceStub.Object, mapperStub.Object)
                {
                    ControllerContext = new ControllerContext
                        {HttpContext = new DefaultHttpContext{User = user}}
                };

            var result = await controller.SaveWeather(It.IsAny<string>());

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task SaveCity_WhenInvalidCity_ThrowsException()
        {
            var mapperStub = new Mock<IMapper>();
            var weatherServiceStub = new Mock<IWeatherService>();
            weatherServiceStub.Setup(e => e.SaveWeather(It.IsAny<string>(), It.IsAny<Guid>()))
                .Throws<CityNotFoundException>();

            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(JwtClaimTypes.Subject, Guid.NewGuid().ToString())
            }));

            var controller = new WeatherController
                (weatherServiceStub.Object, mapperStub.Object)
                {
                    ControllerContext = new ControllerContext
                        {HttpContext = new DefaultHttpContext {User = user}}
                };

            var result = controller.SaveWeather(It.IsAny<string>());

            await Assert.ThrowsAsync<CityNotFoundException>(() => result);
        }

        [Fact]
        public async Task SaveCity_WhenAlreadyAssigned_ThrowsException()
        {
            var mapperStub = new Mock<IMapper>();
            var weatherServiceStub = new Mock<IWeatherService>();
            weatherServiceStub.Setup(e => e.SaveWeather(It.IsAny<string>(), It.IsAny<Guid>()))
                .Throws<CityAlreadyAssignedException>();
            
            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(JwtClaimTypes.Subject, Guid.NewGuid().ToString())
            }));

            var controller = new WeatherController
                (weatherServiceStub.Object, mapperStub.Object)
                {
                    ControllerContext = new ControllerContext
                        {HttpContext = new DefaultHttpContext {User = user}}
                };

            var result = controller.SaveWeather(It.IsAny<string>());

            await Assert.ThrowsAsync<CityAlreadyAssignedException>(() => result);
        }

        [Fact]
        public async Task DeleteWeather_ByDefault_ReturnsCorrectType()
        {
            var mapperStub = new Mock<IMapper>();
            var weatherServiceStub = new Mock<IWeatherService>();
            weatherServiceStub.Setup
                    (e => e.DeleteWeather(It.IsAny<string>(), It.IsAny<Guid>()))
                .Returns(Task.FromResult<ICollection<WeatherDto>>(new List<WeatherDto>()));
            
            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(JwtClaimTypes.Subject, Guid.NewGuid().ToString())
            }));

            var controller = new WeatherController
                (weatherServiceStub.Object, mapperStub.Object)
                {
                    ControllerContext = new ControllerContext
                        {HttpContext = new DefaultHttpContext {User = user}}
                };

            var result = await controller.DeleteWeather(It.IsAny<string>());

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteWeather_WhenCityNotAssigned_ThrowsException()
        {
            var mapperStub = new Mock<IMapper>();
            var weatherServiceStub = new Mock<IWeatherService>();
            weatherServiceStub.Setup
                    (e => e.DeleteWeather(It.IsAny<string>(), It.IsAny<Guid>()))
                .Throws<CityNotAssignedException>();
            
            var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(JwtClaimTypes.Subject, Guid.NewGuid().ToString())
            }));

            var controller = new WeatherController
                (weatherServiceStub.Object, mapperStub.Object)
                {
                    ControllerContext = new ControllerContext
                        {HttpContext = new DefaultHttpContext {User = user}}
                };

            var result = controller.DeleteWeather(It.IsAny<string>());

            await Assert.ThrowsAsync<CityNotAssignedException>(() => result);
        }
    }
}