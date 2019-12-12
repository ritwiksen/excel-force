using ExcelForce.Foundation.EntityManagement.Models.Conditionals;

namespace ExcelForce.Foundation.EntityManagement.Models.Expressions
{
    public class Expression
    {
        ExpressionConditions Condition { get; set; }

        ISearchExpression CurrentExpression { get; set; }

        Expression ExpressionAhead { get; set; }
    }
}
