using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebClient.Models;

namespace WebClient.Services
{
    public interface IWeatherApiClient
    {
        Task<WeatherDto> GetWeather(string city);
        Task<ICollection<WeatherDto>> GetUserWeather(string accessToken);
        Task<WeatherDto> SaveCity(string city, string accessToken);
        Task<ICollection<WeatherDto>> DeleteCity(string city, string accessToken);
        Task<WeatherDto> GetForecast(string city);

    }
}