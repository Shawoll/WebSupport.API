using WebSupport.API.Models;

namespace WebSupport.API.Models
{
    public class HeaderOptions : AuthApiModel
    {
        public string Path { get; set; }

        public string Method { get; set; }
    }
}
