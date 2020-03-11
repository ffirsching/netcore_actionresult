using System;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces
{
    interface IWeatherForecastService
    {
        Task<WeatherForecast> GetOne(int id);
        Task<WeatherForecast> GetList();
    }
}
