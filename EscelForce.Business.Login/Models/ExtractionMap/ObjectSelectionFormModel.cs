using System.Collections.Generic;

namespace ExcelForce.Business.Models.ExtractionMap
{
    public class ObjectSelectionFormModel
    {
       public IEnumerable<string> ObjectNames { get; set; }

        public string selectedObjectName { get; set; }
    }
}
