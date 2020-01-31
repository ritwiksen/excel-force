using ExcelForce.Foundation.Authentication.Models;
using ExcelForce.Foundation.CoreServices.Authentication;
using ExcelForce.Foundation.CoreServices.Models;
using ExcelForce.Foundation.CoreServices.ServiceCallWrapper.Interfaces;
using ExcelForce.Foundation.Persistence.Persitence;
using System;
using System.Collections.Generic;

namespace ExcelForce.Foundation.Authentication.Services
{
    public class SalesforceAuthenticationManager : IAuthenticationManager<AuthenticationRequest, AuthenticationResponse>
    {
        private readonly IServiceCallWrapper<AuthenticationResponse, ApiError> _loginServiceCallWrapper;

        private readonly IPersistenceContainer _persistenceContainer;

        private const string _salesforcePassword = "password";


        public SalesforceAuthenticationManager(IServiceCallWrapper<AuthenticationResponse, ApiError> loginServiceCallWrapper,
            IPersistenceContainer persistenceContainer)
        {
            _loginServiceCallWrapper = loginServiceCallWrapper;

            _persistenceContainer = persistenceContainer;
        }

        public AuthenticationResponse Login(AuthenticationRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var payload = new Dictionary<string, string>
            {
                {"grant_type",_salesforcePassword},
                {"client_id",request.ConsumerKey},
                {"client_secret",request.SecretKey},
                {"username","nissankulatejaswi@deloitte.com.excelforce"},
                {"password","Excelforce@12345bubYCTWjdofbg5xBcuXZkkDhR" }
            };

            var apiRequest = new AuthenticationApiRequest()
            {
                FormEncodedPostData = payload
            };

            //TODO:(Ritwik):: Get these URL's from a configuration file
            var host = _persistenceContainer?.ApiConfigurationManager.Get()?.GetUrl();

            var url = $"{host}services/oauth2/token";

            var response = _loginServiceCallWrapper.Post(url, apiRequest)?.Result;

            return response?.Model;
        }
        public AuthenticationResponse Logout(string AccessToken,string InstanceUrl)
        {

            //TODO:(Ritwik):: Get these URL's from a configuration file
            // var host = _persistenceContainer?.ApiConfigurationManager.Get()?.GetUrl();


            var url = $"{InstanceUrl}/services/oauth2/revoke?token={AccessToken}";
            var requestObject = new AuthenticationApiRequest()
            {
                Headers = new Dictionary<string, string>
                {
                }

            };


            var response = _loginServiceCallWrapper.Get(url, requestObject)?.Result;

            return response?.Model;

        }
    }
}
