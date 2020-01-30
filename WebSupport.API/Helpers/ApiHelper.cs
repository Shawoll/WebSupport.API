using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;

namespace WebSupport.API.Helpers
{
    public static class ApiHelper
    {
        public static HttpClient HttpClient { get; set; }

        private static string api = "https://rest.websupport.sk";

        public static HttpClient InitializeClient(string id, string sec, string p, string m)
        {
            HttpClient = new HttpClient();
            HttpClient.BaseAddress = new Uri(api);
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", GetBase64EncodedString(id,sec,p,m));
            HttpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpClient.DefaultRequestHeaders.AcceptCharset.Add(new StringWithQualityHeaderValue("utf-8"));
            HttpClient.DefaultRequestHeaders.Date = DateHeader();
            HttpClient.DefaultRequestHeaders.Add("User-Agent", "WebSupport.API.Wrapper");
            return HttpClient;
        }

        private static string GetBase64EncodedString(string id, string sec, string p, string m)
        {
            HMACSHA1 hmscha = new HMACSHA1(Encoding.ASCII.GetBytes(sec));
            string cnncl = CreateCanonical(m, p);
            byte[] canonical = Encoding.ASCII.GetBytes(cnncl);
            byte[] signatureByte = hmscha.ComputeHash(canonical);
            string signatureString = string.Join("", Array.ConvertAll(signatureByte, b => b.ToString("x2")));
            byte[] signature = Encoding.ASCII.GetBytes($"{id}:{signatureString}");
            string signatureBase64 = Convert.ToBase64String(signature);

            return signatureBase64;
        }

        private static long UnixTimeNowTest() => (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;

        private static DateTimeOffset DateHeader() => DateTime.Parse(DateTime.UtcNow.ToString("s"));

        private static string CreateCanonical(string method, string path) => $"{method} {path} {UnixTimeNowTest()}";
    }
}

