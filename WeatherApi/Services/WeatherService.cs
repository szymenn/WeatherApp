using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WeatherApi.Data;
using WeatherApi.Entities;
using WeatherApi.Exceptions;
using WeatherApi.Models;

namespace WeatherApi.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IMapper _mapper;
        private readonly IDbContext _context;
        private readonly IWeatherApiClient _apiClient;

        public WeatherService(IMapper mapper, IDbContext context, IWeatherApiClient apiClient)
        {
            _mapper = mapper;
            _context = context;
            _apiClient = apiClient;
        }

        public async Task<WeatherDto> GetWeather(string city)
        {
            return await _apiClient.GetWeatherDto(city);
        }

        public async Task<WeatherDto> SaveWeather(string city, Guid userId)
        {
            if (IsAlreadyAssigned(city, userId))
            {
                throw new CityAlreadyAssignedException
                (Microsoft.AspNetCore.Http.StatusCodes.Status409Conflict, 
                    "Conflict",
                    "City is already assigned to that user");
            }

            return await SaveWeatherEntity(city, userId);
        }

        public async Task<ICollection<WeatherDto>> GetUserWeather(Guid userId)
        {
            var userWeatherData = _context.WeatherData.Where(p => p.UserId == userId).ToList();

            var weatherDtos = await GetWeatherDtos(userWeatherData);
            return weatherDtos;
        }

        public async Task<ICollection<WeatherDto>> DeleteWeather(string city, Guid userId)
        {
            DeleteWeatherEntity(city, userId);
            
            return await GetUserWeather(userId);
        }

        public async Task<WeatherDto> GetForecast(string city)
        {
            return await _apiClient.GetForecastDto(city);
        }
        
        private async Task<ICollection<WeatherDto>> GetWeatherDtos(IEnumerable<Weather> userWeatherData)
        {
            var weatherDtos = new List<WeatherDto>();

            foreach (var userWeather in userWeatherData)
            {
                var weatherDto = await _apiClient.GetWeatherDto(userWeather.City);
                weatherDtos.Add(weatherDto);
            }
            
            return weatherDtos;
        }
        
        private bool IsAlreadyAssigned(string city, Guid userId)
        {
            city = city.ToLower();
            return _context.WeatherData.Any(p => p.City == city && p.UserId == userId);
        }

        private void DeleteWeatherEntity(string city, Guid userId)
        {
            city = city.ToLower();
            var weather = _context.WeatherData.SingleOrDefault(p => p.City == city && p.UserId == userId);
            if (weather == null)
            {
                throw new CityNotAssignedException
                (Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound, 
                    "Not Found",
                    "City not assigned to that user.");   
            }

            _context.WeatherData.Remove(weather);
            _context.SaveChanges();
        }

        private async Task<WeatherDto> SaveWeatherEntity(string city, Guid userId)
        {
            var weatherDto = await _apiClient.GetWeatherDto(city);
            var weather = _mapper.Map<Weather>(weatherDto);
            weather.UserId = userId;
 
            _context.WeatherData.Add(weather);
            _context.SaveChanges();

            return weatherDto;
        }
    }
}
