using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace alexander_neumann.api.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        [HttpGet]
        public ActionResult<IEnumerable<WeatherForecast>> Get(int numberOfItems = 5)
        {
            try
            {
                if (numberOfItems < 0)
                    throw new ArgumentOutOfRangeException("numberOfItems must be zero or greater.");

                var rng = new Random();
                return Enumerable.Range(1, numberOfItems).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("/oidcconfig")]
        public async Task<ActionResult<string>> GetOIDCConfig()
        {
            try
            {
                var url = "https://alexanderneumann.b2clogin.com/alexanderneumann.onmicrosoft.com/B2C_1_signin1/v2.0/.well-known/openid-configuration";

                var request = new HttpRequestMessage(HttpMethod.Get, url);

                var client = _clientFactory.CreateClient();

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return content;
                }
                else
                {
                    return $"{response.StatusCode.ToString()}: {response.ReasonPhrase}";
                }
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
