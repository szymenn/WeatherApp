using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebClient.Exceptions;
using WebClient.Helpers;
using WebClient.Models;

namespace WebClient.Services
{
    public class WeatherApiClient : IWeatherApiClient
    {
        private readonly HttpClient _httpClient;

        public WeatherApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WeatherDto> GetWeather(string city)
        {
            var response = await _httpClient.GetAsync($"weather/{city}");
            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = await response.Content.ReadAsAsync<ProblemDetails>();
                throw new WeatherApiException(problemDetails);
            }

            return await response.Content.ReadAsAsync<WeatherDto>();
        }

        public async Task<ICollection<WeatherDto>> GetUserWeather(string accessToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue(Constants.AuthenticationScheme, accessToken);

            var response = await _httpClient.GetAsync($"/weather");
            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = await response.Content.ReadAsAsync<ProblemDetails>();
                throw new WeatherApiException(problemDetails);   
            }

            return await response.Content.ReadAsAsync<ICollection<WeatherDto>>();
        }
        
        public async Task<WeatherDto> SaveCity(string city, string accessToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue(Constants.AuthenticationScheme, accessToken);
            
            var response = await _httpClient.PostAsync
                ($"/weather/{city}", new StringContent($"{city}"));

            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = await response.Content.ReadAsAsync<ProblemDetails>();
                throw new WeatherApiException(problemDetails);                        
            }

            return await response.Content.ReadAsAsync<WeatherDto>();
        }
        
        public async Task<ICollection<WeatherDto>> DeleteCity(string city, string accessToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(Constants.AuthenticationScheme, accessToken);

            var response = await _httpClient.DeleteAsync($"/weather/{city}");
            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = await response.Content.ReadAsAsync<ProblemDetails>();
                throw new WeatherApiException(problemDetails);
            }

            return await response.Content.ReadAsAsync<ICollection<WeatherDto>>();
        }

        public async Task<WeatherDto> GetForecast(string city)
        {
            var response = await _httpClient.GetAsync($"/weather/forecast/{city}");
            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = await response.Content.ReadAsAsync<ProblemDetails>();
                throw new WeatherApiException(problemDetails);
            }

            return await response.Content.ReadAsAsync<WeatherDto>();
        }
    }
}