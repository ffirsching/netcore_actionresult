using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IWeatherForecastService
    {
        Task<WeatherForecast> GetOne(int id);
        Task<IEnumerable<WeatherForecast>> GetList();
    }
}
