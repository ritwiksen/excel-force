using ExcelForce.Foundation.EntityManagement.Models.SfEntities;

namespace ExcelForce.Foundation.EntityManagement.Models.Conditionals
{
    public class SearchExpression<T> : ISearchExpression
    {
        public SfField Field { get; set; }

        public Conditions Condition { get; set; }

        public T Value { get; set; }
    }

    public interface ISearchExpression
    {

    }
}
