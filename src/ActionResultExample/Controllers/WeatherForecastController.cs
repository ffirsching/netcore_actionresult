using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Application.Interfaces;
using System.Threading.Tasks;

namespace ActionResultExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
    

        private readonly IWeatherForecastService _repo;

        public WeatherForecastController(IWeatherForecastService repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
           return await _repo.GetList();
        }
    }
}
