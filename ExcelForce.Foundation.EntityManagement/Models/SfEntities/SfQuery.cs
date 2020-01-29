using System.Collections.Generic;
using System.Linq;

namespace ExcelForce.Foundation.EntityManagement.Models.SfEntities
{
    public class SfQuery
    {
        public string Name { get; set; }

        public IList<SfObject> Objects { get; set; }

        public SfObject ParentObject { get; set; }
        public SfObject GetParentObject() => Objects?.First(x => x.IsPrimary) ?? null;

        public IList<SfObject> GetChildren() => Objects?.Where(x => !x.IsPrimary)?.ToList() ?? null;

    }
}
