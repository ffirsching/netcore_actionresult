using Application.Models;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IWeatherForecastService
    {
        Task<WeatherForecast> Create(CreateWeatherForecastDto forecast);
        Task<WeatherForecast> GetOne(int id);
        Task<List<WeatherForecast>> GetList();
    }
}
