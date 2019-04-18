using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebClient.Helpers;
using WebClient.Models;
using WebClient.Services;
using Constants = WebClient.Helpers.Constants;

namespace WebClient.Controllers
{
    [Authorize]
    public class WeatherController : Controller
    {
        private readonly IMapper _mapper;
        private readonly WeatherApiClient _apiClient;

        public WeatherController(
            WeatherApiClient apiClient,
            IMapper mapper)
        {
            _apiClient = apiClient;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var accessToken = await HttpContext.GetTokenAsync(Constants.AccessToken);

            var weatherDtos = await _apiClient.GetUserWeather(accessToken);
            var weatherViewModels = _mapper.Map<ICollection<WeatherViewModel>>(weatherDtos);
            var weatherModel = new WeatherModel();
            weatherModel.WeatherViewModels = weatherViewModels;

            return View(weatherModel);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> City(WeatherBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var weatherDto = await _apiClient.GetWeather(model.City);
            var weatherViewModel = _mapper.Map<WeatherViewModel>(weatherDto);

            return View(new CityWeatherModel
            {
                WeatherViewModel = weatherViewModel
            });
        }

        [HttpPost]
        public async Task<IActionResult> SaveCity(WeatherBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var accessToken = await HttpContext.GetTokenAsync(Constants.AccessToken);
            
            var weatherDto = await _apiClient.SaveCity(model.City, accessToken);
       
            var weatherViewModel = _mapper.Map<WeatherViewModel>(weatherDto);
            return View(weatherViewModel);

        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Forecast(string city)
        {
            if (city == null)
            {
                return NotFound();
            } 
            
            var weatherDto = await _apiClient.GetForecast(city);
            var weatherForecastViewModel = _mapper.Map<WeatherForecastViewModel>(weatherDto);
            return View(weatherForecastViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(WeatherBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Index));
            }

            var accessToken = await HttpContext.GetTokenAsync(Constants.AccessToken);
            var updatedWeather = await _apiClient.DeleteCity(model.City, accessToken);
            var updatedWeatherViewModel = _mapper.Map<ICollection<WeatherViewModel>>(updatedWeather);

            return View(nameof(Index), new WeatherModel
            {
                WeatherViewModels = updatedWeatherViewModel
            });

        }

        public IActionResult Test()
        {
            return View();
        }
        
        public IActionResult Logout()
        {
            return SignOut
                (CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectDefaults.AuthenticationScheme);
        }
    }
}