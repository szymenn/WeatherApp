using System.Net.Http;
using System.Threading.Tasks;
using WeatherApi.Exceptions;
using WeatherApi.Models;

namespace WeatherApi.Services
{
    public class WeatherApiClient : IWeatherApiClient
    {
        private readonly HttpClient _httpClient;

        public WeatherApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WeatherDto> GetWeatherDto(string city)
        {
            var response = await GetWeatherResponseData(city);
            if (!response.IsSuccessStatusCode)
            {
                var errorDto = await response.Content.ReadAsAsync<ErrorDto>();
                throw new CityNotFoundException((int) response.StatusCode,
                    response.ReasonPhrase,
                    errorDto.Error.Message);
            }
            var weatherDto = await response.Content.ReadAsAsync<WeatherDto>();
            return weatherDto;
        }

        public async Task<WeatherDto> GetForecastDto(string city)
        {
            var response = await GetForecastResponseData(city);

            if (!response.IsSuccessStatusCode)
            {
                var errorDto = await response.Content.ReadAsAsync<ErrorDto>();
                throw new ForecastException((int)response.StatusCode,
                    response.ReasonPhrase,
                    errorDto.Error.Message);
            }

            var weatherDto = await response.Content.ReadAsAsync<WeatherDto>();
            return weatherDto;
        }

        private async Task<HttpResponseMessage> GetWeatherResponseData(string city)
        {
            var response = await _httpClient.GetAsync
                ($"/v1/current.json?key=8901d4953b024df288f115913191602&q={city}");

            return response;
        }

        private async Task<HttpResponseMessage> GetForecastResponseData(string city)
        {
            var response = await _httpClient.GetAsync
                ($"v1/forecast.json?key=8901d4953b024df288f115913191602&q={city}&days=7");

            return response;
        }
    }
}