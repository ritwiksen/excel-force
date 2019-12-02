using System.Collections.Generic;

namespace ExcelForce.Foundation.CoreServices.ServiceCallWrapper.Interfaces
{
    public interface IPostData
    {
        IDictionary<string, string> FormEncodedPostData { get; set; }
    }
}
