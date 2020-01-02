using ExcelForce.Business.Constants;
using ExcelForce.Business.Interfaces;
using ExcelForce.Foundation.Authentication.Models;
using ExcelForce.Foundation.CoreServices.Authentication;
using ExcelForce.Foundation.CoreServices.Logger.Interfaces;
using ExcelForce.Foundation.CoreServices.Models;
using ExcelForce.Foundation.CoreServices.Repository;
using ExcelForce.Foundation.Persistence.Persitence;
using ExcelForce.Foundation.ProfileManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExcelForce.Business.Services.UserAuthentication
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly IAuthenticationManager<AuthenticationRequest, AuthenticationResponse> _authenticationManager;

        private readonly IExcelForceRepository<ConnectionProfile, string> _connectionProfileRepository;

        private readonly IPersistenceContainer _persistenceContainer;

        private readonly ILoggerManager _loggerManager;

        public UserAuthenticationService(IAuthenticationManager<AuthenticationRequest, AuthenticationResponse> authenticationManager,
            IExcelForceRepository<ConnectionProfile, string> connectionProfileRepository,
            IPersistenceContainer persistenceContainer,
            ILoggerManager loggerManager)
        {
            _authenticationManager = authenticationManager;

            _connectionProfileRepository = connectionProfileRepository;

            _persistenceContainer = persistenceContainer;

            _loggerManager = loggerManager;
        }

        public ServiceResponseModel<bool> Login(string userName, string password, string securityToken, string connectionProfile)
        {
            List<string> errorList = null;

            var response = false;

            try
            {
                var profileRecord = _connectionProfileRepository.GetRecords()?.FirstOrDefault(
                        x => string.Equals(x.Name, connectionProfile, StringComparison.InvariantCultureIgnoreCase));

                var request = new AuthenticationRequest
                {
                    Username = userName,
                    Password = password,
                    ConsumerKey = profileRecord?.ConsumerKey,
                    SecretKey = profileRecord?.ClientSecret,
                    SecurityToken = securityToken
                };

                var apiconfiguration = _persistenceContainer.ApiConfigurationManager?.Get();

                apiconfiguration.IsEnvironmentProduction = profileRecord?.IsProduction;

                _persistenceContainer.ApiConfigurationManager?.Set(apiconfiguration);

                var authResponse = _authenticationManager.Login(request);

                if (string.IsNullOrWhiteSpace(authResponse?.ErrorMessage) &&
                    !string.IsNullOrWhiteSpace(authResponse?.AccessToken)
                    && !string.IsNullOrWhiteSpace(authResponse?.InstanceUrl))
                {
                    _persistenceContainer.Set(BusinessConstants.AuthResponse, authResponse);

                    response = true;
                }
                else
                {
                    errorList.Add(authResponse.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                LogException(ex, "An error occurred while Logging in", errorList);
            }

            return new ServiceResponseModel<bool>
            {
                Messages = errorList,
                Model = response
            };
        }

        public ServiceResponseModel<bool> Logout() {
            List<string> errorList = null;

            var response = false;
            try
            {
                var loginResponse = _persistenceContainer.Get<AuthenticationResponse>(BusinessConstants.AuthResponse);
                var authResponse = _authenticationManager.Logout(loginResponse?.AccessToken,loginResponse?.InstanceUrl);
            }
            catch (Exception ex) {
                LogException(ex, "An error occurred while Logging in", errorList);
            }

            return new ServiceResponseModel<bool>
            {
                Messages = errorList,
                Model = response
            };
        }

        //TODO:(Get this method to be reused
        private void LogException(Exception ex, string errorMessage, IList<string> errorList)
        {
            errorList.Add("An error occurred while fetching field details");

            _loggerManager.LogError($"{ex.Message} {ex.StackTrace}");
        }


    }
}
