using ExcelForce.Foundation.EntityManagement.Interfaces.ServiceInterfaces;
using ExcelForce.Foundation.EntityManagement.Models.SfEntities;
using System;
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

            if (!string.IsNullOrWhiteSpace(filterExpression))
            {
                selectStatementBuilder = selectStatementBuilder.Append($" ORDER BY {filterExpression}");
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
            var parentSelectItems = string.Join(",", query.GetParentObject());

            var childSelectItems = string.Join(",", query.GetChildren());

            var selectItems = string.Concat(parentSelectItems, childSelectItems);

            return selectItems?.Trim()?.Remove(selectItems.Length - 2, 1);
        }

        private string GetSearchExpression(SfQuery query)
        {
            var filterExpression = query?.GetParentObject()?.FilterExpressions;

            if (query.GetChildren()?.Any() ?? false)
            {
                var childItemExpressions = string.Join("AND ", query.GetChildren().Select(x => x.FilterExpressions));

                filterExpression = string.Concat(filterExpression, " AND", childItemExpressions);
            }

            return filterExpression;
        }

        private string GetSortExpression(SfQuery query)
        {
            var sortExpression = query?.GetParentObject()?.SortExpressions;

            if (query.GetChildren()?.Any() ?? false)
            {
                var childItemExpressions = string.Join(",", query.GetChildren().Select(x => x.SortExpressions));

                sortExpression = string.Concat(sortExpression, ",", childItemExpressions);
            }

            return sortExpression;
        }
    }
}
