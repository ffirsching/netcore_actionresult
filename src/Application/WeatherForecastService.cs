using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application
{
    public class WeatherForecastService: IWeatherForecastService
    {
        private readonly IDbContext _dbContext;
        public WeatherForecastService(IDbContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<WeatherForecast>> GetList()
        {
            return await _dbContext.WeatherForecast.ToListAsync();
        }

        public async Task<WeatherForecast> GetOne(int id)
        {
            return await _dbContext.WeatherForecast.FindAsync(id);
        }
    }
}
