using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ExcelForce.Foundation.CoreServices.Models;
using ExcelForce.Foundation.CoreServices.Models.Enums;
using ExcelForce.Foundation.CoreServices.ServiceCallWrapper.Interfaces;
using Newtonsoft.Json;

namespace ExcelForce.Foundation.CoreServices.ServiceCallWrapper
{
    public class ServiceCallWrapper<TModel, TErrorModel> : IServiceCallWrapper<TModel, TErrorModel>
    {
        private readonly IWebApiHttpClient _webApiHttpClient;

        private string _endpoint;

        public ServiceCallWrapper(IWebApiHttpClient webApiHttpClient)
        {
            _webApiHttpClient = webApiHttpClient;
        }

        public async Task<ApiResponse<TModel, TErrorModel>> Post<T>(string endpoint, T model) where T : IHeader, IPostData
        {
            return await ProcessRequest(endpoint, model, HttpVerb.Post, HandleDefaultResponse, model.FormEncodedPostData).ConfigureAwait(false);
        }

        public async Task<ApiResponse<TModel, TErrorModel>> Get<T>(string endpoint, T model) where T : IHeader
        {
            return await ProcessRequest(endpoint, model, HttpVerb.Get, HandleDefaultResponse).ConfigureAwait(false);
        }

        private async Task<ApiResponse<TModel, TErrorModel>> ProcessRequest<T>(
          string endpoint,
          T model,
          HttpVerb verb,
          Func<HttpResponseMessage, ApiResponse<TModel, TErrorModel>> responseDelegate,
          IDictionary<string, string> postData = null,
          bool allowNullResponse = false) where T : IHeader
        {
            try
            {
                HttpResponseMessage response = null;

                switch (verb)
                {
                    case HttpVerb.Get:
                        response = await _webApiHttpClient
                                .GetResponse(endpoint, model.Headers)
                                .ConfigureAwait(false);
                        break;
                    case HttpVerb.Post:
                        response = await _webApiHttpClient
                                  .PostAsync(endpoint, postData, model.Headers)
                                  .ConfigureAwait(false);
                        break;
                    case HttpVerb.Put:
                        break;
                    case HttpVerb.Patch:
                        break;
                    case HttpVerb.Delete:
                        break;
                    default:
                        break;
                }

                var test = responseDelegate(response);
                return test;
            }
            catch (WebException webEx)
            {
                //Implementation for web exception goes in here
                return null;
            }
            catch (Exception ex)
            {
                //Implementation for normal exception goes in here
                return null;
            }
        }

        private ApiResponse<TModel, TErrorModel> HandleDefaultResponse(HttpResponseMessage response)
        {
            if (response == null)
            {
                throw new InvalidOperationException(
                    $"Response is null while trying to post to endpoint - {_endpoint}");
            }

            if (!response.IsSuccessStatusCode)
            {
                // return HandleFailedResponse(response, _endpoint);
            }

            var result = response
                .Content
                .ReadAsStringAsync()
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();

            return GenerateResponse(
               HttpStatusCode.OK,
               JsonConvert.DeserializeObject<TModel>(result),
               default(TErrorModel),
               false);
        }

        private static ApiResponse<TModel, TErrorModel> GenerateResponse(
          HttpStatusCode statusCode,
          TModel model,
          TErrorModel errorResponse,
          bool allowNullResponse = false)
        {
            if (statusCode != HttpStatusCode.OK && EqualityComparer<TErrorModel>.Default.Equals(errorResponse, default(TErrorModel)))
            {
                throw new ArgumentException("Must provide valid error response when service call wrapper encounters error.");
            }

            if (!allowNullResponse
                     && (statusCode == HttpStatusCode.OK
                               && !typeof(TModel).IsValueType
                               && EqualityComparer<TModel>.Default.Equals(model, default(TModel))))
            {
                throw new ArgumentException("Model can't be null for a successful response.");
            }

            return new ApiResponse<TModel, TErrorModel>
            {
                StatusCode = statusCode,
                Model = model,
                Error = statusCode == HttpStatusCode.OK
                    ? default(TErrorModel)
                    : errorResponse
            };
        }
    }
}
