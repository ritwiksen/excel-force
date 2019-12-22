using ExcelForce.Foundation.EntityManagement.Interfaces.ServiceInterfaces;
using ExcelForce.Foundation.EntityManagement.Models.SfEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExcelForce.Foundation.EntityManagement.Services
{
    public class SfQueryService : ISfQueryService
    {
        public string GetStringifiedQuery(SfQuery query)
        {
            var itemList = GetItemListForQuery(query);

            var selectStatementBuilder = new StringBuilder();

            selectStatementBuilder = selectStatementBuilder.Append($"SELECT {itemList} FROM {query.GetParentObject()?.Name}");

            var filterExpression = GetSearchExpression(query);

            if (!string.IsNullOrWhiteSpace(filterExpression))
            {
                selectStatementBuilder = selectStatementBuilder.Append($" WHERE {filterExpression}");
            }

            var sortExpression = GetSortExpression(query);

            if (!string.IsNullOrWhiteSpace(sortExpression))
            {
                selectStatementBuilder = selectStatementBuilder.Append($" ORDER BY {sortExpression}");
            }

            return selectStatementBuilder?.ToString();
        }

        public bool IsValidQuery(SfQuery query)
        {
            throw new NotImplementedException();
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
                string.Join(",", query.GetParentObject()?.Fields?.Select(x => x.Name)),

                string.Join(",", query.GetChildren()?.SelectMany(x => x.Fields?.Select(y => $"{x.Name}.{y.Name}")))
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
    }
}
