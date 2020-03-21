using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using System.Linq;
using System;
using Domain.Entities;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        private static readonly string[] Summaries = new[]
  {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };


        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {

            services.AddDbContext<InMemoryDbContext>(options => options.UseInMemoryDatabase(databaseName: "WeatherForecastDb"));

            services.AddScoped<IDbContext>(provider => provider.GetService<InMemoryDbContext>());

            return services;
        }

        public static void AddTestData(this IApplicationBuilder app, IServiceProvider provider)
        {
            var context = provider.GetService<InMemoryDbContext>(); //.ApplicationServices.GetService<InMemoryDbContext>();

            if (context != null)
            {
                var rng = new Random();
                var data = Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Id = index,
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();

                context.WeatherForecast.AddRange(data);
                context.SaveChanges();
            }
        }
    }
}