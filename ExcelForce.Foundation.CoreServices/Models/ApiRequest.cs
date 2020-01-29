using ExcelForce.Foundation.CoreServices.ServiceCallWrapper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelForce.Foundation.CoreServices.Models
{
    public class ApiRequest : IHeader
    {
        public IDictionary<string, string> Headers { get; set; }
    }
}