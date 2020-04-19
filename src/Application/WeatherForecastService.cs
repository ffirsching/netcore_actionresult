using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly IDbContext _dbContext;
        public WeatherForecastService(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<WeatherForecast> GetOne(int id)
        {
            return await _dbContext.WeatherForecast.FindAsync(id);
        }
        public async Task<List<WeatherForecast>> GetList()
        {
            return await _dbContext.WeatherForecast.ToListAsync();
        }

        public async Task<WeatherForecast> Create(CreateWeatherForecastDto request)
        {
            var entity = new WeatherForecast()
            {
                Date = request.Date,
                TemperatureC = request.TemperatureC,
                Summary = request.Summary
            };

            _dbContext.WeatherForecast.Add(entity);

            await _dbContext.SaveChangesAsync();

            return entity;
        }

    }
}
