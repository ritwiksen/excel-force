using ExcelForce.Foundation.CoreServices.Models;
using ExcelForce.Foundation.CoreServices.ServiceCallWrapper.Interfaces;
using ExcelForce.Foundation.EntityManagement.Interfaces.ServiceInterfaces;
using ExcelForce.Foundation.EntityManagement.Models.Api.SfObject;
using ExcelForce.Foundation.Persistence.Persitence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExcelForce.Foundation.EntityManagement.Services
{
    public class SfObjectService : ISfObjectService
    {
        private readonly IServiceCallWrapper<SfObjectApiResponse, ApiError> _getSfObjectServiceCallWrapper;

        private readonly IPersistenceContainer _persistenceContainer;

        public SfObjectService(IServiceCallWrapper<SfObjectApiResponse, ApiError> getSfObjectServiceCallWrapper,
            IPersistenceContainer persistenceContainer)
        {
            _getSfObjectServiceCallWrapper = getSfObjectServiceCallWrapper;

            _persistenceContainer = persistenceContainer;
        }

        public IEnumerable<string> GetObjectNames(string bearerToken)
        {
            if (string.IsNullOrWhiteSpace(bearerToken))
                throw new ArgumentNullException(nameof(bearerToken));

            var endpoint = _persistenceContainer?.ApiConfigurationManager?.Get()?.GetUrl();

            endpoint = $"{endpoint}services/data/v43.0/sobjects";

            var requestObject = new SfObjectApiRequest
            {
                Headers = new Dictionary<string, string>
                {
                    { "authorization",$"Bearer {bearerToken}"}
                }
            };

            var response = _getSfObjectServiceCallWrapper.Get(endpoint, requestObject)?.Result;

            if (response.Error != null)
            {
                return response?.Model?.SalesforceObjects?.Select(x => x.Name)?.ToList();
            }

            throw new Exception("An error occurred while fetching salesforce object names");
        }
    }
}
