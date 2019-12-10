using ExcelForce.Foundation.CoreServices.Authentication;

namespace ExcelForce.Foundation.Authentication.Models
{
    public class AuthenticationRequest : IAuthenticationRequest
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string SecurityToken { get; set; }

        public string ConsumerKey { get; set; }

        public string SecretKey { get; set; }

        public bool IsProduction { get; set; }
    }
}
