using AutoMapper;
using WebClient.Models;

namespace WebClient.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<WeatherDto, WeatherViewModel>();
            CreateMap<WeatherDto, WeatherForecastViewModel>();
        }
    }
}