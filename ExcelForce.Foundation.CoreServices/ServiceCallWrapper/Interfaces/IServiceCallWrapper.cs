using ExcelForce.Foundation.CoreServices.Models;
using System.Threading.Tasks;

namespace ExcelForce.Foundation.CoreServices.ServiceCallWrapper.Interfaces
{
    public interface IServiceCallWrapper<TModel, TErrorModel>
    {
        Task<ApiResponse<TModel, TErrorModel>> Post<T>(string endpoint, T model) where T : IHeader, IPostData;

        Task<ApiResponse<TModel, TErrorModel>> Get<T>(string endpoint, T model) where T : IHeader;
    }
}
