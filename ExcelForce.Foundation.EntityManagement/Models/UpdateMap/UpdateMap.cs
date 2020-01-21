using ExcelForce.Foundation.CoreServices.Models.Interfaces;
using ExcelForce.Foundation.EntityManagement.Models.SfEntities;
using System.Collections.Generic;

namespace ExcelForce.Foundation.EntityManagement.Models.UpdateMap
{
    public class UpdateMap : IExcelForceModel
    {
        public string Name { get; set; }

        public SfObject ParentObject { get; set; }

        public IEnumerable<SfObject> ChildObjects { get; set; }
    }
}
