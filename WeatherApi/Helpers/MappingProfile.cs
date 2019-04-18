using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WeatherApi.Entities;
using WeatherApi.Models;

namespace WeatherApi.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<WeatherDto, WeatherApiModel>()
                .ForPath(dest => dest.Location.City,
                    options => options.MapFrom(src => src.Location.Name))
                .ForPath(dest => dest.Location.Country,
                    options => options.MapFrom(src => src.Location.Country))
                .ForPath(dest => dest.Current.TempC,
                    options => options.MapFrom(src => src.Current.TempC))
                .ForPath(dest => dest.Current.TempF,
                    options => options.MapFrom(src => src.Current.TempF))
                .ForPath(dest => dest.Current.Wind,
                    options => options.MapFrom(src => src.Current.Wind))
                .ForPath(dest => dest.Current.Condition.Icon,
                    options => options.MapFrom(src => src.Current.Condition.Icon));
            CreateMap<WeatherDto, Weather>()
                .ForMember(dest => dest.City,
                    options => options.MapFrom(src => src.Location.Name.ToLower()))
                .ForMember(dest => dest.Country,
                    options => options.MapFrom(src => src.Location.Country));
            CreateMap<WeatherDto, WeatherForecastApiModel>()
                .ForPath(dest => dest.Location.City,
                    options => options.MapFrom(src => src.Location.Name))
                .ForPath(dest => dest.Location.Country,
                    options => options.MapFrom(src => src.Location.Country))
                .ForPath(dest => dest.Current.TempC,
                    options => options.MapFrom(src => src.Current.TempC))
                .ForPath(dest => dest.Current.TempF,
                    options => options.MapFrom(src => src.Current.TempF))
                .ForPath(dest => dest.Current.Wind,
                    options => options.MapFrom(src => src.Current.Wind))
                .ForPath(dest => dest.Forecast.ForecastDays,
                    options => options.MapFrom(src => src.Forecast.ForecastDays));
            

        }
    }
}
