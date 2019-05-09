using System;
using Microsoft.EntityFrameworkCore;
using WeatherApi.Entities;

namespace WeatherApi.Data
{
    public interface IDbContext : IDisposable
    {
        DbSet<Weather> WeatherData { get; set; }
        int SaveChanges ();
    }
}