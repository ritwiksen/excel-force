using System.Collections.Generic;
using System.Linq;

namespace ExcelForce.Foundation.EntityManagement.Models.SfEntities
{
    public class SfObject
    {
        public string Name { get; set; }

        IEnumerable<SfField> Fields { get; set; }

        public IList<SfFilterExpression> FilterExpressions { get; set; }

        public IList<SfSortExpression> SortExpressions { get; set; }

        public bool IsPrimary { get; set; }

        public IEnumerable<SfField> GetSystemFields() => Fields?.Where(x => !x.IsCustom);

        public IEnumerable<SfField> GetCustomFields() => Fields?.Where(x => x.IsCustom);
    }
}
