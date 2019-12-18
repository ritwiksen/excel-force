using ExcelForce.Business.Interfaces;
using ExcelForce.Foundation.Authentication.Models;
using ExcelForce.Foundation.CoreServices.Authentication;
using ExcelForce.Foundation.CoreServices.Repository;
using ExcelForce.Foundation.Persistence.Persitence;
using ExcelForce.Foundation.ProfileManagement.Models;
using System;
using System.Linq;

namespace ExcelForce.Business.Services.UserAuthentication
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly IAuthenticationManager<AuthenticationRequest, AuthenticationResponse> _authenticationManager;

        private readonly IExcelForceRepository<ConnectionProfile, string> _connectionProfileRepository;

        private readonly IPersistenceContainer _persistenceContainer;

        public UserAuthenticationService(IAuthenticationManager<AuthenticationRequest, AuthenticationResponse> authenticationManager,
            IExcelForceRepository<ConnectionProfile, string> connectionProfileRepository,
            IPersistenceContainer persistenceContainer)
        {
            _authenticationManager = authenticationManager;

            _connectionProfileRepository = connectionProfileRepository;

            _persistenceContainer = persistenceContainer;
        }

        public bool Login(string userName, string password, string securityToken, string connectionProfile)
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

            return true;
        }
    }
}
