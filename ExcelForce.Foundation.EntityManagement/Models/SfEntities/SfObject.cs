using System.Collections.Generic;
using System.Linq;

namespace ExcelForce.Foundation.EntityManagement.Models.SfEntities
{
    public class SfObject
    {
        public string Name { get; set; }

        public string ApiName { get; set; }

        public IEnumerable<SfField> Fields { get; set; }

        public string FilterExpressions { get; set; }

        public string SortExpressions { get; set; }

        public bool IsPrimary { get; set; }

        public string DisplayName() => GetDisplayName(Name, ApiName);
        public static string GetDisplayName(string name, string apiName) => $"{name} | {apiName}";

        public static string GetObjectNameFromDisplayName(string objectName) => objectName != null && objectName.Split('|').Length > 0 ? objectName.Split('|')[0].Trim() : null;
        public static string GetApiNameFromDisplayName(string objectName) => objectName != null ? (objectName.Split('|').Length > 1) ? objectName.Split('|')[1].Trim() : (objectName.Split('|').Length > 0 ? objectName.Split('|')[0].Trim():null):null;
    }
}
