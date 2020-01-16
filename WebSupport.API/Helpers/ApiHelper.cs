  
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebSupport.API.Helpers
{

  public static class ApiHelper
  {
  
    public static HttpClient ApiClient {get;set;}
    
    private readonly string Api = "https://rest.websupport.sk";
    
    public static void InitializeClient()
    {
      ApiClient = new HttpClient();
      ApiClient.BaseAddress = new Uri(Api);
      ApiClient.DefaultRequestHeaders.Accept.Clear();
      ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }
  }
}
