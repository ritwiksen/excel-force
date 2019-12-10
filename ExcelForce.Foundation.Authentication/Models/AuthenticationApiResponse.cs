using Newtonsoft.Json;

namespace ExcelForce.Foundation.Authentication.Models
{
    public class AuthenticationApiResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }
}
