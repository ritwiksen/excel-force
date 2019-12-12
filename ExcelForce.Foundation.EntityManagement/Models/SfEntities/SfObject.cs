using System.Collections.Generic;

namespace ExcelForce.Foundation.EntityManagement.Models.SfEntities
{
    public class SfObject
    {
        public string Name { get; set; }

        IEnumerable<SfField> SystemFields { get; set; }

        IEnumerable<SfField> CustomFields { get; set; }
    }
}
