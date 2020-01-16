using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;

namespace WebSupport.API.Helpers
{
    public static class ApiHelper
    {

        public static HttpClient ApiClient { get; set; }

        public static string Api = "https://rest.websupport.sk";
        public static string path = "/v1/user/self";

        private static readonly DateTime time = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, TimeZoneInfo.Local.Id, "Greenwich Standard Time");
        private static string time2 = DateTime.UtcNow.ToString("s", System.Globalization.CultureInfo.InvariantCulture);
        private static string method = "GET";
        private static string api = "https://rest.websupport.sk";

        private static readonly string apiKey = "";
        private static readonly string secret = "";

        private static readonly string canonicalRequest = method + " " + path + " " + UnixTimeNow().ToString();
        //private static readonly string authScheme = "Basic";
        private static string signature = CreateToken(canonicalRequest, secret);
        public static string signature2 = Encode(canonicalRequest, Encoding.ASCII.GetBytes(secret));

        public static void InitializeClient()
        {
            var base64String = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{apiKey}:{signature}"));
            var base64String2 = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{apiKey}:{signature2}"));

            ApiClient = new HttpClient();
            ApiClient.BaseAddress = new Uri(Api);
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            ApiClient.DefaultRequestHeaders.Date = DateTime.Parse(time2);
            ApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64String);
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

        public static string Encode(string input, byte[] key)
        {
            var myhmacsha1 = new HMACSHA1(key);
            var byteArray = Encoding.ASCII.GetBytes(input);
            var stream = new MemoryStream(byteArray);
            var hashValue = myhmacsha1.ComputeHash(stream);
            return string.Join("", Array.ConvertAll(hashValue, b => b.ToString("x2")));
        }

        public static long UnixTimeNow()
        {
            var timeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
            return (long)timeSpan.TotalSeconds;
        }
    }
}
