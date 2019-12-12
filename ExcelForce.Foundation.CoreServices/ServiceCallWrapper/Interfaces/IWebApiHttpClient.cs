using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ExcelForce.Foundation.CoreServices.ServiceCallWrapper.Interfaces
{
    public interface IWebApiHttpClient
    {
        Task<HttpResponseMessage> PostAsync(string endPoint, IDictionary<string, string> model, IDictionary<string, string> headers);

        Task<HttpResponseMessage> GetResponse(string endPoint, IDictionary<string, string> headers);
    }
}
