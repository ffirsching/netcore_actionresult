using System;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;

namespace Application
{
    public class WeatherForecastService: IWeatherForecastService
    {
        public WeatherForecastService() {}

        public Task<WeatherForecast> GetList() {
            throw new NotImplementedException();
        }

        public Task<WeatherForecast> GetOne(int id)
        {
            throw new NotImplementedException();
        }
    }
}
