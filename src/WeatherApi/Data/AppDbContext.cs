using Microsoft.EntityFrameworkCore;
using WeatherApi.Entities;

namespace WeatherApi.Data
{
    public class AppDbContext : DbContext, IDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Weather> WeatherData { get; set; }
    }
}
