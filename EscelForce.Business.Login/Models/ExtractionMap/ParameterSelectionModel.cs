using ExcelForce.Foundation.EntityManagement.Models.SfEntities;
using System.Collections.Generic;

namespace ExcelForce.Business.Models.ExtractionMap
{
    public class ParameterSelectionModel
    {
        public string SearchExpression { get; set; }

        public string SortExpression { get; set; }

        public bool IsPrimary { get; set; }

        public bool AddChild { get; set; }

        public List<SfChildRelationship> ChildList { get; set; }

        public string SelectedChild { get; set; }
    }
}
