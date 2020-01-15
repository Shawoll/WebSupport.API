using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
namespace WebSupport.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebSupportApiController : BaseController
    {
        private static readonly DateTime time = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, TimeZoneInfo.Local.Id, "Greenwich Standard Time");
        private static string time2 = DateTime.UtcNow.ToString("s", System.Globalization.CultureInfo.InvariantCulture);
        private static string method = "GET";
        private static string path = "/v1/user/self";
        private static string api = "https://rest.websupport.sk";
        private static readonly string apiKey = "your-key";
        private static readonly string secret = "your-secret";

        private static readonly string canonicalRequest = HttpMethod.Get + path + time;
        private static readonly string authScheme = "Basic";
        private static string signature = CreateToken(canonicalRequest, secret);

        private readonly ILogger<WebSupportApiController> _logger;

        public WebSupportApiController(ILogger<WebSupportApiController> logger)
        { 
            _logger = logger; 
        }

        // GET: api/WebSupportApi 
        [HttpGet]
        public string Get()
        {
            return "WebSupport.API";
        } 

        [HttpGet("{page}/{pageSize}", Name = "GetAllUsers")]
        public string GetAllUsers(int? page, int? pageSize)
        {
            var client = new HttpClient();
            var base64String = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{apiKey}:{signature}"));
            client.BaseAddress = new Uri(api);
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            //client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Date = DateTime.Parse(time2);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authScheme, base64String);

            var result = client.GetStringAsync(path).Result;
            //using (var r = await client.GetAsync(path))
            //{
            //    using (var u = r.Content)
            //    {
            //        string t = await u.ReadAsStringAsync();
            //    }
            //}
            //var request = client.GetStreamAsync(path);

            return result;
        }

        [HttpGet("{id}", Name = "GetUserDetail")]
        public string GetUserDetail(int? id)
        {
            return "Get User Detail";
        }

        [HttpGet("{page}/{pageSize}", Name = "GetAllUserProfiles")]
        public string GetAllUserProfiles(int? page, int? pageSize)
        {
            return "Get All User Profiles";
        }

        [HttpGet("{id}", Name = "GetBillingProfileDetail")]
        public string GetBillingProfileDetail(int id)
        {
            return "GetBillingProfileDetail";
        }

        [HttpPost("{user}", Name = "CreateNewUser")]
        public string CreateNewUser(object user)
        {
            return "Get User Detail";
        }

        [HttpPost("{user}", Name = "CreateNewBillingProfile")]
        public string CreateNewBillingProfile(object user)
        {
            return "Create New Billing Profile";
        }

        [HttpPut("{id}")]
        public void UpdateUser(int id, [FromBody] string value)
        {
        }

        [HttpPut("{id}", Name = "UpdateBillingProfile")]
        public void UpdateBillingProfile(int id, [FromBody] string value)
        {
        }

        [HttpPost]
        public bool PasswordReset()
        {
            return true;
        }

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
        [HttpDelete("{id}")] public void Delete(int id) 
        { 
        }

        // DELETE: api/ApiWithActions/5 
        [HttpDelete("{id}")]
        public void DeleteBillingProfile(int id)
        {
        }

        private static string CreateToken(string message, string secret)
        {
            secret ??= string.Empty;
            var encoding = new ASCIIEncoding();
            var keyByte = encoding.GetBytes(secret);
            var messageBytes = encoding.GetBytes(message);
            using var hmacSha256 = new HMACSHA1(keyByte);
            var hashMessage = hmacSha256.ComputeHash(messageBytes);
            return Convert.ToBase64String(hashMessage);
        }

    }
}
