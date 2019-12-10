using ExcelForce.Foundation.Authentication.Models;
using ExcelForce.Foundation.CoreServices.Authentication;
using ExcelForce.Foundation.CoreServices.ServiceCallWrapper.Interfaces;
using System;
using System.Collections.Generic;

namespace ExcelForce.Foundation.Authentication.Services
{
    public class SalesforceAuthenticationManager : IAuthenticationManager<AuthenticationRequest, AuthenticationResponse>
    {
        private readonly IServiceCallWrapper<AuthenticationApiResponse, ErrorModel> _loginServiceCallWrapper;

        public SalesforceAuthenticationManager(IServiceCallWrapper<AuthenticationApiResponse, ErrorModel> loginServiceCallWrapper)
        {
            _loginServiceCallWrapper = loginServiceCallWrapper;
        }

        public AuthenticationResponse Login(AuthenticationRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var payload = new Dictionary<string, string>
            {
                {"grant_type",request.Password},
                {"client_id",request.ConsumerKey},
                {"client_secret",request.SecretKey},
                {"username",request.Username},
                {"password",request.SecurityToken }
            };

            var apiRequest = new AuthenticationApiRequest()
            {
                FormEncodedPostData = payload
            };

            //TODO:(Ritwik):: Get these URL's from a configuration file
            var url = request.IsProduction ? "https://login.salesforce.com/services/oauth2/token" : "https://test.salesforce.com/services/oauth2/token";

            var response = _loginServiceCallWrapper.Post(url, apiRequest)?.Result?.Model;

            return MapApiResponseToLoginResponse(response);
        }

        private AuthenticationResponse MapApiResponseToLoginResponse(AuthenticationApiResponse response)
        {
            return new AuthenticationResponse
            {
                AccessToken = response?.AccessToken
            };
        }
    }

    public class ErrorModel
    {

    }
}
