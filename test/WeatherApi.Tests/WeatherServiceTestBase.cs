using System;
using Microsoft.EntityFrameworkCore;
using WeatherApi.Data;
using WeatherApi.Entities;

namespace WeatherApi.Tests
{
    public class WeatherServiceTestBase : IDisposable
    {
        protected AppDbContext Context { get; }

        protected WeatherServiceTestBase()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            Context = new AppDbContext(options);

            Context.Database.EnsureCreated();

            Initialize(Context);
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }

        private void Initialize(AppDbContext context)
        {
            var weatherData = new[]
            {
                new Weather {City = "berlin", UserId = Guid.Parse("46b41624-0050-4221-a722-f06914f3f152")},
                new Weather{City = "new York", UserId = Guid.Parse("46b41624-0050-4221-a722-f06914f3f152")},
                new Weather{City = "moscow", UserId = Guid.Parse("46b41624-0050-4221-a722-f06914f3f152")},
                new Weather{City = "warsaw", UserId = Guid.Parse("f83582da-06e3-469a-93ad-4b452264124c")},
                new Weather{City = "london", UserId = Guid.Parse("f83582da-06e3-469a-93ad-4b452264124c")},
                new Weather{City = "rome", UserId = Guid.Parse("316bc987-05e9-4651-bb37-67cf2bd70c7d")},
                new Weather{City = "tokyo", UserId = Guid.Parse("316bc987-05e9-4651-bb37-67cf2bd70c7d")},
                new Weather{City = "warsaw", UserId = Guid.Parse("31035f07-4524-4adc-b0cf-1a53d4eb3fb1")},
                new Weather{City = "berlin", UserId = Guid.Parse("31035f07-4524-4adc-b0cf-1a53d4eb3fb1")},
            };
            
            context.WeatherData.AddRange(weatherData);
            context.SaveChanges();
        }
    }
}