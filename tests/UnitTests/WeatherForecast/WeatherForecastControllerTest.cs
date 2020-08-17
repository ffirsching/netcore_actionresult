using ActionResultExample.Controllers;
using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTests.WeatherForecasts
{
    public class WeatherForecastControllerTest : IDisposable
    {

        private readonly WeatherForecastDbFixture fixture;
        private readonly WeatherForecastController controller;

        public WeatherForecastControllerTest()
        {
            // prepare db and test data
            fixture = new WeatherForecastDbFixture();

            // mock repo service
            var forecastService = new Mock<IWeatherForecastService>();
            forecastService.Setup(s => s.GetList()).ReturnsAsync(fixture.forecastList);
            forecastService.Setup(s => s.GetOne(It.IsAny<int>()))
            .ReturnsAsync((int id) => fixture.forecastList.Where(forecast => forecast.Id == id).FirstOrDefault());
            forecastService.Setup(s => s.Create(It.IsAny<CreateWeatherForecastDto>())).ReturnsAsync((CreateWeatherForecastDto createRequest) =>
            {
                var newWeatherForecast = new WeatherForecast()
                {
                    Id = fixture.DbContext.WeatherForecast.Count() + 1,
                    Date = createRequest.Date,
                    TemperatureC = createRequest.TemperatureC,
                    Summary = createRequest.Summary
                };

                fixture.DbContext.Add(newWeatherForecast);
                fixture.DbContext.SaveChanges();
                fixture.forecastList.Add(newWeatherForecast);

                return newWeatherForecast;
            });

            // create controller to test
            controller = new WeatherForecastController(forecastService.Object);
        }

        [Fact(DisplayName = "Should create new weather forecast")]
        public async void Shoud_Create_New_Forecast()
        {
            // arrange
            var createRequest = new CreateWeatherForecastDto()
            {
                Date = DateTime.Today,
                TemperatureC = 14,
                Summary = "humid"
            };

            // act
            var result = await controller.Create(createRequest);

            // assert
            var expected = fixture.forecastList.Last();

            Assert.IsAssignableFrom<IActionResult>(result);
            Assert.IsType<CreatedResult>(result); // check correct return type

            var actual = (CreatedResult) result;
            Assert.Equal(expected, actual.Value); // check correct return value

            Assert.Equal("/" + expected.Id, actual.Location); // check correct location
        }

        [Fact(DisplayName = "Should return all weather forecasts")]
        public async void Should_Return_All_Forecasts()
        {
            // arrange
            var expected = fixture.forecastList;

            // act
            var result = await controller.GetList();

            // assert
            Assert.IsType<ActionResult<List<WeatherForecast>>>(result);
            Assert.Equal(expected, result.Value); // Using Value since ReturnType is ActionResult<List<WeatherForecast>>, so we need to access the value
        }

        [Fact(DisplayName = "Should return correct weather forecast by id")]
        public async void Should_Return_Forecast_By_Id()
        {
            // arrange
            var expected = fixture.forecastList.Where(forecast => forecast.Id == 1).FirstOrDefault();

            // act
            var result = await controller.GetOne(1);

            // assert
            Assert.IsType<ActionResult<WeatherForecast>>(result);
            Assert.Equal(expected, result.Value); // Using Value since ReturnType is ActionResult<WeatherForecast>, so we need to access the value
        }

        [Fact(DisplayName = "Should return NotFound()")]
        public async void Should_Return_NotFound()
        {
            // act
            var result = await controller.GetOne(999);

            // assert
            Assert.Null(result.Value);
            Assert.IsType<ActionResult<WeatherForecast>>(result); // check method return type matches
            Assert.IsType<NotFoundResult>(result.Result); // check actual return is as expected
        }

        public void Dispose()
        {
            fixture.Dispose();
        }
    }
}
