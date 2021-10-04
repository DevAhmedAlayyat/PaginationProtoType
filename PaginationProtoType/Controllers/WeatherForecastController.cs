using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PaginationProtoType.Dtos;
using PaginationProtoType.Dtos.Parameters;
using PaginationProtoType.Infrastructure;
using PaginationProtoType.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaginationProtoType.Controllers
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
        private readonly ApplicationDbContext _context;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("jobs")]
        public async Task<ActionResult<Paging<Job>>> GetJobs([FromQuery] JobParameters parameters)
        {
            var data = await _context.Jobs.Skip(parameters.PageSize * (parameters.PageNumber - 1)).Take(parameters.PageSize).ToListAsync();
            var dataCount = await _context.Jobs.CountAsync();
            return Ok(new Paging<Job>(parameters.PageNumber, dataCount / parameters.PageSize, parameters.PageSize, dataCount, data));
        }
    }
}
