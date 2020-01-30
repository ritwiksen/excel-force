using ExcelForce.Foundation.CoreServices.Models;
using ExcelForce.Foundation.CoreServices.ServiceCallWrapper.Interfaces;
using ExcelForce.Foundation.EntityManagement.Interfaces.ServiceInterfaces;
using ExcelForce.Foundation.EntityManagement.Models.ExtractMap;
using ExcelForce.Foundation.EntityManagement.Models.SfEntities;
using ExcelForce.Foundation.CoreServices.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExcelForce.Foundation.EntityManagement.Services
{
    public class SfQueryService : ISfQueryService
    {
        private readonly IServiceCallWrapper<SfExtractDataWrapper, ApiError> _loginServiceCallWrapper;
        public SfQueryService(IServiceCallWrapper<SfExtractDataWrapper, ApiError> loginServiceCallWrapper)
        {
            _loginServiceCallWrapper = loginServiceCallWrapper;
        }
        public string GetStringifiedQuery(ExtractMap query)
        {
            if (query?.Query == null)
                throw new ArgumentNullException("query cannot be null");

            var queryObject = query.Query;

            var resultQuery = GetQueryForReadableObject(query.Query.Parent, true);

            if (queryObject.Children?.Any() ?? false)
            {
                var childQueries = query.Query?.Children
                    ?.Select(x => $"({GetQueryForReadableObject(x)})");

                resultQuery = resultQuery.Replace("[child]", string.Join(",", childQueries));
            }
            else
            {
                resultQuery = resultQuery.Replace(",[child]", string.Empty);
            }

            return resultQuery;
        }

        public bool IsValidQuery(SfQuery query)
        {
            throw new NotImplementedException();
        }

        private string GetQueryForReadableObject(ReadableObject readableQueryObject, bool isParent = false)
        {
            var parentAdditionalContent = isParent ? ",[child]" : string.Empty;
            var selectStatement = $"SELECT {string.Join(",", readableQueryObject?.Fields?.Select(x => x.ApiName))}{parentAdditionalContent}";

            var queryBuilder = new StringBuilder();


            if (string.IsNullOrWhiteSpace(readableQueryObject.RelationshipName))
            {
                queryBuilder.Append($"{selectStatement} FROM {readableQueryObject.Label}");
            }
            else {
                queryBuilder.Append($"{selectStatement} FROM {readableQueryObject.RelationshipName}");
            }

            if (!string.IsNullOrWhiteSpace(readableQueryObject?.SearchFilter))
                queryBuilder.Append($" WHERE {readableQueryObject.SearchFilter}");

            if (!string.IsNullOrWhiteSpace(readableQueryObject?.SortFilter))
                queryBuilder.Append("{readableQueryObject.SortFilter}");

            return queryBuilder.ToString();
        }

        public SfQuery MapStringifiedQuery(string query)
        {
            var indexOfSelect = query.IndexOf("select", StringComparison.InvariantCultureIgnoreCase) + ("select").Length;

            var indexOfFrom = query.IndexOf("from", StringComparison.InvariantCultureIgnoreCase);

            var selectItemsSection = query.Substring(indexOfSelect, indexOfFrom - indexOfSelect)?.Trim();

            var selecttItems = selectItemsSection.Split(',');

            selecttItems.Select(x =>
            {
                var parsedName = x.Split('.');

                return new
                {
                    Name = parsedName.Length > 1 ? parsedName[1] : parsedName[0],
                    ObjectName = parsedName.Length > 1 ? parsedName[0] : string.Empty
                };
            });

            return null;
        }

        private string GetItemListForQuery(SfQuery query)
        {
            var querySections = new List<string>
            {
                string.Join(",", query.GetParentObject()?.Fields?.Select(x => x.ApiName)),

                string.Join(",", query.GetChildren()?.SelectMany(x => x.Fields?.Select(y => $"{x.Name}.{y.ApiName}")))
            };

            querySections.RemoveAll(x => string.IsNullOrWhiteSpace(x.Trim()));

            return string.Join(",", querySections);
        }

        private string GetSearchExpression(SfQuery query)
        {
            var filterExpression = query?.GetParentObject()?.FilterExpressions;

            if (query.GetChildren()?.Any(x => !string.IsNullOrWhiteSpace(x.FilterExpressions)) ?? false)
            {
                var childItemExpressions = string.Join("AND ",
                    query.GetChildren()?.Where(x => string.IsNullOrWhiteSpace(x.FilterExpressions))?.Select(x => x.FilterExpressions));

                if (!string.IsNullOrWhiteSpace(childItemExpressions))
                {
                    filterExpression = string.Concat(filterExpression, " AND", childItemExpressions);
                }
            }

            return filterExpression;
        }

        private string GetSortExpression(SfQuery query)
        {
            var expressions = new List<string>
            {
                query?.GetParentObject()?.SortExpressions
            };

            if (query.GetChildren()?.Any() ?? false)
            {
                expressions.AddRange(query.GetChildren().Select(x => x.SortExpressions));
            }

            expressions.RemoveAll(x => string.IsNullOrWhiteSpace(x));

            return string.Join(",", expressions);
        }
        public SfExtractDataWrapper ExtractData(string query, string AccessToken, string InstanceUrl)
        {

            var url = $"{InstanceUrl}/services/data/v47.0/query?q={query}";
            var token = $"Bearer {AccessToken}";
            var requestObject = new ApiRequest()
            {
                Headers = new Dictionary<string, string>
                {
                    { "Authorization" , token}
                }

             };
    var response = _loginServiceCallWrapper.Get(url, requestObject)?.Result;

            return response?.Model;


        }
    }
}
