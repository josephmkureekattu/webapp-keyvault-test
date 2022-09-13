using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace WebApplication1.Controllers
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
        private readonly IConfiguration configuration;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration configuration)
        {
            _logger = logger;
            this.configuration = configuration;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            //using (var context = new AppDbContext(configuration))
            //{
            //    Book book = new Book { Title = "sdfsdf", Authors = new Author[] { new Author { Name = "dsytfgy342r53i" }, new Author { Name = "435" } } };
            //    context.Book.Add(book);
            //    context.SaveChanges();
            //}

            using (var context = new AppDbContext(configuration))
            {
                context.Database.EnsureCreated();
                var mno = context.Author.Where(x => x.Name == "dsytfgy342r53i" + System.DateTime.UtcNow.ToString()).ToList();
            }

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}