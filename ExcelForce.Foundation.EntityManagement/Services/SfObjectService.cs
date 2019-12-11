using ExcelForce.Foundation.CoreServices.Models;
using ExcelForce.Foundation.CoreServices.ServiceCallWrapper.Interfaces;
using ExcelForce.Foundation.EntityManagement.Interfaces.ServiceInterfaces;
using ExcelForce.Foundation.EntityManagement.Models.Api.SfObject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExcelForce.Foundation.EntityManagement.Services
{
    public class SfObjectService : ISfObjectService
    {
        private readonly IServiceCallWrapper<SfObjectApiResponse, ApiError> _getSfObjectServiceCallWrapper;

        public SfObjectService(IServiceCallWrapper<SfObjectApiResponse, ApiError> getSfObjectServiceCallWrapper)
        {
            _getSfObjectServiceCallWrapper = getSfObjectServiceCallWrapper;
        }

        public IEnumerable<string> GetObjectNames(string bearerToken)
        {
            if (string.IsNullOrWhiteSpace(bearerToken))
                throw new ArgumentNullException(nameof(bearerToken));

            var endpoint = "https://login.salesforce.com/services/oauth2/token/services/data/v43.0/sobjects";

            var response = _getSfObjectServiceCallWrapper.Get(endpoint, new SfObjectApiRequest())?.Result;

            if (response.Error != null)
            {
                return response?.Model?.SalesforceObjects?.Select(x => x.Name)?.ToList();
            }

            throw new Exception("An error occurred while fetching salesforce object names");
        }
    }

    public class ErrorResponse
    {
    }
}
