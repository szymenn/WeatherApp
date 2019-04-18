using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeatherApi.Services;
using WeatherApi.Models;

namespace WeatherApi.Controllers
{
    [Route("[Controller]")]
    [Authorize]
    public class WeatherController : ControllerBase
    {
        private readonly WeatherService _weatherService;
        private readonly IMapper _mapper;

        public WeatherController(WeatherService weatherService, IMapper mapper)
        {
            _weatherService = weatherService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserWeather()
        {
            var userId = Guid.Parse(User.FindFirstValue(JwtClaimTypes.Subject));

            var userWeatherData = await _weatherService.GetUserWeather(userId);

            var userWeatherModel = _mapper.Map<ICollection<WeatherApiModel>>(userWeatherData);
            return Ok(userWeatherModel);
        }

        [HttpGet("{city}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetWeather(string city)
        {
            var weather = await _weatherService.GetWeather(city);
            var weatherModel = _mapper.Map<WeatherApiModel>(weather);
            return Ok(weatherModel); 
        }

        [HttpPost("{city}")]
        public async Task<IActionResult> SaveWeather(string city)
        {
            var userId = Guid.Parse(User.FindFirstValue(JwtClaimTypes.Subject));
            var weatherDto = await _weatherService.SaveWeather(city, userId);

            var weatherModel = _mapper.Map<WeatherApiModel>(weatherDto);
            return Ok(weatherModel);
        }

        [HttpDelete("{city}")]
        public async Task<IActionResult> DeleteWeather(string city)
        {
            var userId = Guid.Parse(User.FindFirstValue(JwtClaimTypes.Subject));

            var updatedWeather = await _weatherService.DeleteWeather(city, userId);

            var updatedWeatherModel = _mapper.Map<ICollection<WeatherApiModel>>(updatedWeather);
            return Ok(updatedWeatherModel);
        }

        [HttpGet("forecast/{city}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetForecast(string city)
        {
            var forecast = await _weatherService.GetForecast(city);

            var forecastApiModel = _mapper.Map<WeatherForecastApiModel>(forecast);
            return Ok(forecastApiModel);
        }
        
    }
}
