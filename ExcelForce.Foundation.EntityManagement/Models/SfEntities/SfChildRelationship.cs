using System.Collections.Generic;

namespace ExcelForce.Foundation.EntityManagement.Models.SfEntities
{
    public class SfChildRelationship
    {
        public string ObjectName { get; set; }

        public IList<string> RelationshipFields { get; set; }
    }
}
