using ExcelForce.Foundation.EntityManagement.Models.Conditionals;

namespace ExcelForce.Foundation.EntityManagement.Models.SfEntities
{
    public class SfFilterExpression
    {
        public string Property { get; set; }

        public Conditions Condition { get; set; }

        public string Value { get; set; }
    }
}
