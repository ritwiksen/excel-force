using ExcelForce.Foundation.CoreServices.ServiceCallWrapper.Interfaces;
using System.Collections.Generic;

namespace ExcelForce.Foundation.Authentication.Models
{
    public class AuthenticationApiRequest : IHeader
    {
        public IDictionary<string, string> Headers { get; set; }
    }
}
