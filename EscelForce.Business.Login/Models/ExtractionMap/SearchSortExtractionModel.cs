using ExcelForce.Foundation.EntityManagement.Models.SfEntities;
using System.Collections.Generic;

namespace ExcelForce.Business.Models.ExtractionMap
{
    public class SearchSortExtractionModel
    {
        public string SearchExpression { get; set; }

        public string SortExpression { get; set; }

        public bool AddChild { get; set; }

        public IList<SfObject> Children { get; set; }

        public IList<SfChildRelationship> ChildRelationships { get; set; }

        public string SelectedChild { get; set; }

        public bool ShowAddChildSection { get; set; }

        public string MapName { get; set; }

        public bool ShowMapNameSection { get; set; }

        public string SelectedChildRelationshipName { get; set; }
    }
}
