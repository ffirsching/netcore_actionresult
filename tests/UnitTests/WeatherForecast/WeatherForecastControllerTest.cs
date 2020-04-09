using ActionResultExample.Controllers;
using Application;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace UnitTests.WeatherForecasts
{
    public class WeatherForecastControllerTest: IDisposable
    {

        private readonly WeatherForecastDbFixture fixture;
        private readonly WeatherForecastController controller;

        public WeatherForecastControllerTest()
        {
            fixture = new WeatherForecastDbFixture();
            Console.WriteLine(fixture.DbContext.WeatherForecast.ToList().ToString());
            var forecastService = new WeatherForecastService(fixture.DbContext);
            controller = new WeatherForecastController(forecastService);
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
