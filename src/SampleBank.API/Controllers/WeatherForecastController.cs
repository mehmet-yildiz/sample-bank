using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SampleBank.Core.Abstractions.Business;
using SampleBank.Core.Entity;

namespace SampleBank.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
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
        public IEnumerable<WeatherForecast> Get()
        {
            _userBusiness.Insert(new User() { Name = "mehmet1", Surname = "yildiz" });

            var list = _userBusiness.GetAll();

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
