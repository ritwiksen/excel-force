using ExcelForce.Foundation.EntityManagement.Models.SfEntities;

namespace ExcelForce.Foundation.EntityManagement.Models.Conditionals
{
    public class SearchExpression<T>
    {
        public SfField<T> Field { get; set; }

        public Conditions Condition { get; set; }

        public T Value { get; set; }
    }
}
