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
    }
}
