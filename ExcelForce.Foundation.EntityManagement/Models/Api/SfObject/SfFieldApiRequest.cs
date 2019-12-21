using ExcelForce.Foundation.CoreServices.ServiceCallWrapper.Interfaces;
using System;
using System.Collections.Generic;

namespace ExcelForce.Foundation.EntityManagement.Models.Api.SfObject
{
    public class SfFieldApiRequest : IHeader
    {
        public IDictionary<string, string> Headers { get; set; }
    }
}
