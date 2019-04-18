using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WeatherApi.Exceptions;
using WeatherApi.Models;

namespace WeatherApi.Helpers
{
    public class WeatherApiClient
    {
        private readonly HttpClient _httpClient;

        public WeatherApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WeatherDto> GetWeatherDto(string city)
        {
            var response = await _httpClient.GetAsync
                ($"/v1/current.json?key=8901d4953b024df288f115913191602&q={city}");
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
            var response = await _httpClient.GetAsync
                ($"v1/forecast.json?key=8901d4953b024df288f115913191602&q={city}&days=7");

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
    }
}