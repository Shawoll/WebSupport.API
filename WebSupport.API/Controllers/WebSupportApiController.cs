using System;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebSupport.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebSupportApiController : ControllerBase
    {
        private readonly ILogger<WebSupportApiController> _logger;

        public WebSupportApiController(ILogger<WebSupportApiController> logger)
        {
            _logger = logger;
        }

        // GET: api/WebSupportApi
        [HttpGet]
        public string Get()
        {
            try
            {
                using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
                {
                    client.BaseAddress = new Uri("https://api.stackexchange.com/2.2/");
                    HttpResponseMessage response = client.GetAsync("answers?order=desc&sort=activity&site=stackoverflow").Result;
                    response.EnsureSuccessStatusCode();
                    string result = response.Content.ReadAsStringAsync().Result;

                    return result;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        // GET: api/WebSupportApi/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/WebSupportApi
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/WebSupportApi/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
