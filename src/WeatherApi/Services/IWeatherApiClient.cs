using System.Net.Http;
using System.Threading.Tasks;
using WeatherApi.Models;

namespace WeatherApi.Services
{
    public interface IWeatherApiClient
    {
        Task<WeatherDto> GetWeatherDto(string city);
        Task<WeatherDto> GetForecastDto(string city);
    }
}