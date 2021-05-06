using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using SampleBank.API.Model;
using SampleBank.Core.Abstractions.Business;
using SampleBank.Core.Entity;

namespace SampleBank.API.Controllers
{
    public class WeatherForecastController : SecureController
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        private readonly IBusinessUser _userBusiness;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IBusinessUser userBusiness)
        {
            _logger = logger;
            _userBusiness = userBusiness;
        }

        [HttpGet]
        public ApiResponse<IEnumerable<WeatherForecast>> Get()
        {
            var response = new ApiResponse<IEnumerable<WeatherForecast>>();
            var rng = new Random();
            var items =  Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
            response.Data = items;
            return response;
        }
    }
}
