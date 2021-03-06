﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebSupport.API.Helpers;

namespace WebSupport.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebSupportApiController : BaseController
    {
        private readonly ILogger<WebSupportApiController> _logger;
        private readonly IConfiguration _config;
        private readonly string apiId;
        private readonly string apiSecret;

        public WebSupportApiController(ILogger<WebSupportApiController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            apiId = _config.GetValue<string>("AuthApiModel:ApiId");
            apiSecret = _config.GetValue<string>("AuthApiModel:ApiSecret");
        }

        // GET: api/WebSupportApi 
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { name = "WebSupport.API.Wrapper" });
        }

        [Route("GetAllUsers")]
        [HttpGet("getallusers/{page:int?}/{pageSize:int?}")]
        [Produces("application/json")]
        public JsonResult GetAllUsers(int? page, int? pageSize)
        {
            var path = "/v1/user/self";

            var client = ApiHelper.InitializeClient(apiId, apiSecret, path, "GET");
            var response = client.GetAsync(path).Result;

            if (response.IsSuccessStatusCode)
            {
                return new JsonResult(response);
            }
            else
            {
                return new JsonResult("oops");
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
