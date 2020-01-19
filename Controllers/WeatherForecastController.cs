using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using Serilog;

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
        private HttpClient _client;
        private RestClient _rest;

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
            _client = _clientFactory.CreateClient();
            _client.Timeout = TimeSpan.FromSeconds(10);

            _rest = new RestClient();
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

        [HttpGet("/httpClient/https")]
        public async Task<ActionResult<HttpResponseMessage>> GetHttpsUrl()
        {
            try
            {
                var url = "https://alexanderneumann.b2clogin.com/alexanderneumann.onmicrosoft.com/B2C_1_signin1/v2.0/.well-known/openid-configuration";

                var request = new HttpRequestMessage(HttpMethod.Get, url);

                var response = await _client.SendAsync(request);

                return response;
            }
            catch(Exception ex)
            {
                var json = JsonConvert.SerializeObject(ex);
                Log.Error(json);
                throw;
            }
        }

        [HttpGet("/httpClient/http")]
        public async Task<ActionResult<HttpResponseMessage>> GetHttpUrl()
        {
            try
            {
                var url = "http://www.usc-muenchen.de/bogensport/";

                var request = new HttpRequestMessage(HttpMethod.Get, url);

                var response = await _client.SendAsync(request);

                return response;
            }
            catch (Exception ex)
            {
                var json = JsonConvert.SerializeObject(ex);
                Log.Error(json);
                throw;
            }
        }

        [HttpGet("/httpClient/url")]
        public async Task<ActionResult<string>> GetUrl(string url)
        {
            if(string.IsNullOrEmpty(url))
                return BadRequest("Url must not be empty");

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);

                var response = await _client.SendAsync(request);

                return await response.Content.ReadAsStringAsync();
            }
            catch(Exception ex)
            {
                var json = JsonConvert.SerializeObject(ex);
                Log.Error(json);
                throw;
            }
        }

        [HttpGet("/restSharp/http")]
        public string GetHttpUrlWithRestSharp()
        {
            try
            {
                var url = "http://www.usc-muenchen.de/bogensport/";

                var request = new RestRequest(url, DataFormat.Json);

                var response = _rest.Get(request);

                return response.Content;
            }
            catch (Exception ex)
            {
                var json = JsonConvert.SerializeObject(ex);
                Log.Error(json);
                throw;
            }
        }

        [HttpGet("/restSharp/https")]
        public string GetHttpsUrlWithRestSharp()
        {
            try
            {
                var url = "https://alexanderneumann.b2clogin.com/alexanderneumann.onmicrosoft.com/B2C_1_signin1/v2.0/.well-known/openid-configuration";

                var request = new RestRequest(url, DataFormat.Json);

                var response = _rest.Get(request);

                return response.Content;
            }
            catch (Exception ex)
            {
                var json = JsonConvert.SerializeObject(ex);
                Log.Error(json);
                throw;
            }
        }
    }
}
