using ExcelForce.Foundation.EntityManagement.Interfaces.ServiceInterfaces;
using ExcelForce.Foundation.EntityManagement.Models.SfEntities;
using System;
using System.Linq;

namespace ExcelForce.Foundation.EntityManagement.Services
{
    public class SfQueryService : ISfQueryService
    {
        public string GetStringifiedQuery(SfQuery query)
        {
            var itemList = GetItemListForQuery(query);

            var selectStatement = $"SELECT {itemList}";

            return selectStatement;
        }

        public bool IsValidQuery(SfQuery query)
        {
            throw new System.NotImplementedException();
        }

        public SfQuery MapStringifiedQuery(string query)
        {
            //var indexOfSelect = query.IndexOf("select", StringComparison.InvariantCultureIgnoreCase) + ("select").Length;

            //var indexOfFrom = query.IndexOf("from", StringComparison.InvariantCultureIgnoreCase);

            //var selectItemsSection = query.Substring(indexOfSelect, indexOfFrom - indexOfSelect)?.Trim();

            //var selecttItems = selectItemsSection.Split(',');

            //selecttItems.Select(x =>
            //{
            //    var parsedName = x.Split('.');

            //    return new
            //    {
            //        Name = parsedName.Length > 1 ? parsedName[1] : parsedName[0],
            //        ObjectName = parsedName.Length > 1 ? parsedName[0] : string.Empty
            //    };
            //});

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
            return null;
        }

        private string GetSortExpression(SfQuery query)
        {
            return null;
        }
    }
}
