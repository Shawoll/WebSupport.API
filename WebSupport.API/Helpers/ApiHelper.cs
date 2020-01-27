using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;

namespace WebSupport.API.Helpers
{
    public static class ApiHelper
    {
        public static HttpClient ApiClient { get; set; }

        private static string api = "https://rest.websupport.sk";

        public static HttpClient InitializeClient(string id, string sec, string p, string m)
        {
            ApiClient = new HttpClient();
            ApiClient.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("Basic", CreateBase64EncodedString(id, sec, p, m));
            ApiClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            ApiClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json");
            ApiClient.DefaultRequestHeaders.Date = DateTime.UtcNow; 
            ApiClient.BaseAddress = new Uri(api);
            return ApiClient;
        }

        private static string CreateBase64EncodedString(string id, string sec, string p, string m)
        {
            HMACSHA1 hmscha = new HMACSHA1(Encoding.ASCII.GetBytes(sec), true);
            string cnncl = CreateCanonical(m, p);
            byte[] canonical = Encoding.ASCII.GetBytes(cnncl);
            byte[] signatureByte = hmscha.ComputeHash(canonical);
            string signatureString = string.Join("", Array.ConvertAll(signatureByte, b => b.ToString("x2")));
            byte[] signature = Encoding.ASCII.GetBytes($"{id}:{signatureString}");
            string signatureBase64 = Convert.ToBase64String(signature);

            return signatureBase64;
        }

        private static long UnixTimeNow()
        {
            var timeSpan = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0);

            return (long)timeSpan.TotalSeconds;
        }

        private static string CreateCanonical(string method, string path)
        {
            var canonical = $"{method} {path} {UnixTimeNow().ToString()}";

            return canonical;
        }
    }
}
