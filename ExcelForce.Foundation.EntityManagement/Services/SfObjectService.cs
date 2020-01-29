using ExcelForce.Foundation.CoreServices.Models;
using ExcelForce.Foundation.CoreServices.ServiceCallWrapper.Interfaces;
using ExcelForce.Foundation.EntityManagement.Interfaces.ServiceInterfaces;
using ExcelForce.Foundation.EntityManagement.Models.Api.SfObject;
using ExcelForce.Foundation.EntityManagement.Models.SfEntities;
using ExcelForce.Foundation.Persistence.Persitence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExcelForce.Foundation.EntityManagement.Services
{
    public class SfObjectService : ISfObjectService
    {
        private readonly IServiceCallWrapper<SfObjectApiResponse, ApiError> _getSfObjectServiceCallWrapper;

        private readonly IServiceCallWrapper<SfFieldApiResponse, ApiError> _getChildrenApiWrapper;

        private readonly IPersistenceContainer _persistenceContainer;

        public SfObjectService(IServiceCallWrapper<SfObjectApiResponse, ApiError> getSfObjectServiceCallWrapper,
             IServiceCallWrapper<SfFieldApiResponse, ApiError> getChildrenApiWrapper,
            IPersistenceContainer persistenceContainer)
        {
            _getSfObjectServiceCallWrapper = getSfObjectServiceCallWrapper;

            _persistenceContainer = persistenceContainer;

            _getChildrenApiWrapper = getChildrenApiWrapper;
        }

        public IEnumerable<SfChildRelationship> GetChildRelationShips(string objectName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SfObject> GetChildrenForObject(string instanceUrl, string bearerToken, string parentObjectName)
        {
            if (string.IsNullOrWhiteSpace(bearerToken))
                throw new ArgumentNullException(nameof(bearerToken));

            if (string.IsNullOrWhiteSpace(instanceUrl))
                throw new ArgumentNullException(nameof(instanceUrl));

            if (string.IsNullOrWhiteSpace(parentObjectName))
                throw new ArgumentNullException(nameof(parentObjectName));

            var requestObject = new SfFieldApiRequest
            {
                Headers = new Dictionary<string, string>
                {
                    { "authorization",$"Bearer {bearerToken}"}
                }
            };

            var endpoint = $"{instanceUrl}/services/data/v43.0/sobjects/{parentObjectName}/describe";

            var response = _getChildrenApiWrapper.Get(endpoint, requestObject)?.Result;

            if (response.Error == null)
            {
                return response?.Model?.Children
                ?.GroupBy(x => x.Name)
                ?.OrderBy(x => x.Key)
                ?.Select(x =>
                     new SfObject
                     {
                         Name = x.First().Name
                     })
                ?.ToList();
            }

            throw new Exception("An error occurred while fetching the details of children");
        }

        public IEnumerable<SfObject> GetObjects(string instanceUrl, string bearerToken)
        {
            if (string.IsNullOrWhiteSpace(bearerToken))
                throw new ArgumentNullException(nameof(bearerToken));

            if (string.IsNullOrWhiteSpace(instanceUrl))
                throw new ArgumentNullException(nameof(instanceUrl));

            var endpoint = $"{instanceUrl}/services/data/v43.0/sobjects";

            var requestObject = new SfObjectApiRequest
            {
                Headers = new Dictionary<string, string>
                {
                    { "authorization",$"Bearer {bearerToken}"}
                }
            };

            var response = _getSfObjectServiceCallWrapper.Get(endpoint, requestObject)?.Result;

            if (response.Error == null)
            {
                return response?.Model?.SalesforceObjects?.Select(x =>
                     new SfObject
                     {
                         Name = x.Label,
                         ApiName=x.Name
                     })?.ToList();
            }

            throw new Exception("An error occurred while fetching salesforce object names");
        }


    }
}
