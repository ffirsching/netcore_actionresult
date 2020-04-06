using Domain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests.WeatherForecasts
{
    public class WeatherForecastDbFixture : IDisposable
    {

        public readonly List<WeatherForecast> forecastList;
        public InMemoryDbContext DbContext { get; private set; }

        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };


        public WeatherForecastDbFixture()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<InMemoryDbContext>();
            builder.UseInMemoryDatabase(databaseName: "WeatherForecastDatabase")
                   .UseInternalServiceProvider(serviceProvider);

            DbContext = new InMemoryDbContext(builder.Options);

            forecastList = new List<WeatherForecast>();

            CreateTestData();
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }

        private void CreateTestData()
        {
            var rng = new Random();
            var data = Enumerable.Range(1, 10).Select(index => new WeatherForecast
            {
                Id = index,
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();

            forecastList.AddRange(data);

            DbContext.WeatherForecast.AddRange(data);
            DbContext.SaveChangesAsync();
        }
    }
}