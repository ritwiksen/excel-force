using ExcelForce.Foundation.CoreServices.ServiceCallWrapper.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ExcelForce.Foundation.CoreServices.ServiceCallWrapper
{
    public class WebApiHttpClient : IWebApiHttpClient
    {
        public async Task<HttpResponseMessage> PostAsync(string endPoint, IDictionary<string, string> model, IDictionary<string, string> headers)
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(endPoint)
            };

            httpClient.DefaultRequestHeaders.Accept.Clear();

            using (httpClient)
            {
                return await httpClient
                    .PostAsync(endPoint, new FormUrlEncodedContent(model))
                    .ConfigureAwait(false);
            }
        }
    }
}
