using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Moq;
using WebClient.Controllers;
using WebClient.Models;
using WebClient.Services;
using Xunit;

namespace WebClient.Tests
{
    public class WeatherControllerTests
    {
        [Fact]
        public async Task Index_Always_ReturnsViewObjectResult()
        {
            var apiClientMock = new Mock<IWeatherApiClient>();
            var mapperMock = new Mock<IMapper>();
            apiClientMock.Setup(e => e.GetUserWeather(It.IsAny<string>()))
                .Returns(Task.FromResult<ICollection<WeatherDto>>(new List<WeatherDto>()));
            mapperMock.Setup(e => e.Map<ICollection<WeatherViewModel>>
                    (It.IsAny<ICollection<WeatherDto>>()))
                .Returns(new List<WeatherViewModel>());
            var fakeAccessToken = "token";
            var contextWrapperMock = new Mock<IHttpContextWrapper>();
            contextWrapperMock.Setup(e => e.GetTokenAsync(It.IsAny<string>(), new DefaultHttpContext()))
                .Returns(Task.FromResult(fakeAccessToken));

            var weatherController = new WeatherController
            (apiClientMock.Object,
                mapperMock.Object,
                contextWrapperMock.Object
            );

            var actual = await weatherController.Index();

            Assert.IsType<ViewResult>(actual);
        }
    }


}