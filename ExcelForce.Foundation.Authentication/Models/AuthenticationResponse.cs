﻿using ExcelForce.Foundation.CoreServices.Authentication;

namespace ExcelForce.Foundation.Authentication.Models
{
    public class AuthenticationResponse : IAuthenticationResponse
    {
        public string AccessToken { get; set; }
    }
}
