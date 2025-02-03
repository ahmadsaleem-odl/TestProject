using Microsoft.AspNetCore.Mvc;

namespace Product_web_api.Controllers
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
        private readonly IConfiguration _config;


        public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

       /* [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }*/
        [HttpGet("GetString")]
        public Task<ActionResult<string>> GetConnectionString()
        {
            string connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");
                
            // If not found, fallback to appsettings.json or local.settings.json
            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = _config.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
            }

            // Fetch the source identifier
            string configSource = Environment.GetEnvironmentVariable("CONFIG_SOURCE") ?? _config.GetSection("TestConfig").GetSection("Source").Value;

            string result = $"ConnectionString: {connectionString}, Source: {configSource}";

            return Task.FromResult<ActionResult<string>>(Ok(result));
        }

    }
}
