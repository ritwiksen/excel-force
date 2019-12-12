using System.ComponentModel;

namespace ExcelForce.Foundation.EntityManagement.Models.Conditionals
{
    public enum Conditions
    {
        [Description("Greater than")]
        GreaterThan,

        [Description("Equal to")]
        EqualTo,

        [Description("Not equal to")]
        NotEqualTo,

        [Description("Less than")]
        LessThan,

        [Description("Greater than and equal to")]
        GreaterThanEqualTo,

        [Description("Less than and equal to")]
        LessThanEqualTo
    }
}
