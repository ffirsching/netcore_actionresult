using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateWeatherForecastDto forecast)
        {
            try
            {
                var createdEntity = await _repo.Create(forecast);

                var uri = $"{Url.PageLink()}/{createdEntity.Id}";

                return Created(uri, createdEntity);
            }
            catch (System.Exception)
            {

                return BadRequest();
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<WeatherForecast>>> GetList()
        {
            var result = await _repo.GetList();
            return result;
        }

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WeatherForecast>> GetOne(int id)
        {
            var result = await _repo.GetOne(id);

            if (result == null)
            {
                return NotFound();
            }

            return result;
        }
    }
}
