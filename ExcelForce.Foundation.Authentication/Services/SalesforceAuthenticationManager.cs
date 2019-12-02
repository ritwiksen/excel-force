using ExcelForce.Foundation.Authentication.Models;
using ExcelForce.Foundation.CoreServices.Authentication;
using ExcelForce.Foundation.CoreServices.ServiceCallWrapper.Interfaces;
using System;
using System.Collections.Generic;

namespace ExcelForce.Foundation.Authentication.Services
{
    public class SalesforceAuthenticationManager : IAuthenticationManager<AuthenticationRequest, AuthenticationResponse>
    {
        private readonly IServiceCallWrapper<AuthenticationApiRequest, ErrorModel> _loginServiceCallWrapper;

        public SalesforceAuthenticationManager(IServiceCallWrapper<AuthenticationApiRequest, ErrorModel> loginServiceCallWrapper)
        {
            _loginServiceCallWrapper = loginServiceCallWrapper;
        }

        public AuthenticationResponse Login(AuthenticationRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var payload = new Dictionary<string, string>
            {
                {"grant_type","password"},
                {"client_id",request.ConsumerKey},
                {"client_secret",request.SecretKey},
                {"username",request.Username},
                {"password",$"{request.Password}{request.SecurityToken}" }
            };

            var apiRequest = new AuthenticationApiRequest()
            {
                Headers = payload
            };

            var url = "https://login.salesforce.com/services/oauth2/token";

            var response = _loginServiceCallWrapper.Post(url, apiRequest)?.Result?.Model;

            return MapApiResponseToLoginResponse(response);
        }

        private AuthenticationResponse MapApiResponseToLoginResponse(AuthenticationApiRequest response)
        {
            throw new NotImplementedException();
        }
    }

    public class ErrorModel
    {

    }
}
