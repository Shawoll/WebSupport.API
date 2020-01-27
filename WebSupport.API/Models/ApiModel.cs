using System;
using System.Net.Http;
using System.Security.Cryptography;

namespace WebSupport.API.Models
{
    public class ApiModel
    {
        public DateTime CurrentTime { get; set; }
        public DateTime Time { get; set; }
        public string Method { get; set; }
        public string Api { get; set; }
        public string Path { get; set; }
        public string CanonicalRequest => Method + Path + Time;
        public HMACSHA1 HMACSHA1 { get; set; }
        public byte SignatureByte { get; set; }
        public string Signature { get; set; }
        public string Base64EncodedString { get; set; }
        public HttpRequestMessage Headers { get; set; }
        public dynamic Parameters { get; set; }
    }
}
