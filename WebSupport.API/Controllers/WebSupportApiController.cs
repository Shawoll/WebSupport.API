using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebSupport.API.Helpers;
using WebSupport.API.Models;

namespace WebSupport.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebSupportApiController : BaseController
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
            return "WebSupport.API";
        }

        [Route("GetAllUsers")]
        [HttpGet("{page:int?}/{pageSize:int?}")]
        public async Task<UserModel> GetAllUsers(int? page, int? pageSize)
        {
            ApiHelper.InitializeClient();

            // C# 8 feature
            using var response = await ApiHelper.ApiClient.GetAsync(ApiHelper.path);

            if (response.IsSuccessStatusCode)
            {
                var user = await response.Content.ReadAsAsync<UserModel>();

                return user;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

        [HttpGet("{id:int?}")]
        public string GetUserDetail(int? id)
        {
            return "Get User Detail";
        }

        [HttpGet("{page:int?}/{pageSize:int?}")]
        public string GetAllUserProfiles(int? page, int? pageSize)
        {
            return "Get All User Profiles";
        }

        [HttpGet("{id:int?}")]
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

        // DELETE: api/ApiWithActions/5 
        [HttpDelete("{id}")]
        public void DeleteBillingProfile(int id)
        {
        }

    }
}
