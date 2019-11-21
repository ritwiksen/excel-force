using ExcelForce.Foundation.CoreServices.ServiceCallWrapper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ExcelForce.Foundation.CoreServices.ServiceCallWrapper
{
    public class WebApiHttpClient : IWebApiHttpClient
    {
        public Task<HttpResponseMessage> PostAsync(string endPoint, IDictionary<string, string> model, IDictionary<string, string> headers)
        {
            throw new NotImplementedException();
        }
    }
}
