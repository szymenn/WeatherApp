using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherApi.Models;

namespace WeatherApi.Services
{
    public interface IWeatherService
    {
        Task<WeatherDto> GetWeather(string city);
        Task<WeatherDto> SaveWeather(string city, Guid userId);
        Task<ICollection<WeatherDto>> GetUserWeather(Guid userId);
        Task<ICollection<WeatherDto>> DeleteWeather(string city, Guid userId);
        Task<WeatherDto> GetForecast(string city);
    }
}