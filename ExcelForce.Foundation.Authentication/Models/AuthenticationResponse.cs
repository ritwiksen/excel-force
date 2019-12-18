using ExcelForce.Foundation.CoreServices.Authentication;
using Newtonsoft.Json;

namespace ExcelForce.Foundation.Authentication.Models
{
    public class AuthenticationResponse : IAuthenticationResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        public string ErrorMessage { get; set; }
    }
}
