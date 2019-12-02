using ExcelForce.Foundation.CoreServices.ServiceCallWrapper.Interfaces;
using System.Collections.Generic;

namespace ExcelForce.Foundation.Authentication.Models
{
    public class AuthenticationApiRequest : IHeader,IPostData
    {
        public IDictionary<string, string> Headers { get; set; }
        public IDictionary<string, string> FormEncodedPostData { get; set; }
    }
}
