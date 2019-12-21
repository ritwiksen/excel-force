using ExcelForce.Foundation.EntityManagement.Models.SfEntities;
using System.Collections.Generic;

namespace ExcelForce.Business.Models.ExtractionMap
{
    public class FieldSelectionModel
    {
        public List<SfField> SfFields { get; set; }

        public List<SfField> AvailableFields { get; set; }

        public string ObjectName { get; set; }
    }
}
