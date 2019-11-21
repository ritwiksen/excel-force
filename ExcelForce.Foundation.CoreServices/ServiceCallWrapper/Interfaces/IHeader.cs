using System.Collections.Generic;

namespace ExcelForce.Foundation.CoreServices.ServiceCallWrapper.Interfaces
{
    public interface IHeader
    {
        IDictionary<string, string> Headers { get; set; }
    }
}
