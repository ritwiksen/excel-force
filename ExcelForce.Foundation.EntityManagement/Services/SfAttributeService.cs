using System;
using System.Collections.Generic;
using System.Linq;
using ExcelForce.Foundation.CoreServices.Models;
using ExcelForce.Foundation.CoreServices.ServiceCallWrapper.Interfaces;
using ExcelForce.Foundation.EntityManagement.Interfaces.ServiceInterfaces;
using ExcelForce.Foundation.EntityManagement.Models.Api.SfObject;
using ExcelForce.Foundation.EntityManagement.Models.SfEntities;
using ExcelForce.Foundation.Persistence.Persitence;

namespace ExcelForce.Foundation.EntityManagement.Services
{
    public class SfAttributeService : ISfAttributeService
    {
        private readonly IServiceCallWrapper<SfFieldApiResponse, ApiError> _fieldApiCallWrapper;

        private readonly IPersistenceContainer _persistenceContainer;

        public SfAttributeService(IServiceCallWrapper<SfFieldApiResponse, ApiError> fieldApiCallWrapper,
            IPersistenceContainer persistenceContainer)
        {
            _fieldApiCallWrapper = fieldApiCallWrapper;

            _persistenceContainer = persistenceContainer;
        }

        public IEnumerable<SfChildRelationship> GetChildRelationships(string objectName, string bearerToken, string instanceUrl)
        {
            var response = PerformSObjectsDescribe(objectName, bearerToken, instanceUrl);

            if (response.Error == null)
            {
                return response?.Model?.Children
                    ?.GroupBy(x => x.Name)
                    ?.Select(x => new SfChildRelationship
                    {
                        ObjectName = x.Key,
                        RelationshipFields = x?.Select(y => y.RelationshipField)?.ToList()
                    })
                ?.ToList();
            }

            throw new Exception("An error occurred while fetching child relationships object names");
        }

        public IEnumerable<SfField> GetSfFields(string objectName, string bearerToken, string instanceUrl)
        {
            var response = PerformSObjectsDescribe(objectName, bearerToken, instanceUrl);

            if (response.Error == null)
            {
                return response?.Model?.Fields?.Select(x => new SfField
                {
                    ApiName = x.Name,
                    Name = x.Label,
                    Type = x.Type,
                    Length = x.Length,
                    IsMandatory=x.IsMandatory
                })
                ?.ToList();
            }

            throw new Exception("An error occurred while fetching salesforce object names");
        }

        private ApiResponse<SfFieldApiResponse, ApiError> PerformSObjectsDescribe(
            string objectName, string bearerToken, string instanceUrl)
        {
            if (string.IsNullOrWhiteSpace(objectName))
                throw new ArgumentNullException(nameof(objectName));

            if (string.IsNullOrWhiteSpace(bearerToken))
                throw new ArgumentNullException(nameof(bearerToken));

            if (string.IsNullOrWhiteSpace(instanceUrl))
                throw new ArgumentNullException(nameof(instanceUrl));

            var requestObject = new SfFieldApiRequest
            {
                Headers = new Dictionary<string, string>
                {
                    { "authorization",$"Bearer {bearerToken}"}
                }
            };

            var endpoint = $"{instanceUrl}/services/data/v43.0/sobjects/{objectName}/describe";

            return _fieldApiCallWrapper.Get(endpoint, requestObject)?.Result;
        }
    }
}
