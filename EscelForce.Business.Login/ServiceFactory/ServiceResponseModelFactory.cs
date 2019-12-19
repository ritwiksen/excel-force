using ExcelForce.Foundation.CoreServices.Models;
using System.Linq;

namespace ExcelForce.Business.ServiceFactory
{
    internal static class ServiceResponseModelFactory
    {
        internal static ServiceResponseModel<T> GetValueTypeModel<T>() where T : struct
        {
            return new ServiceResponseModel<T>
            {
                Model = default(T),
                Messages = null
            };
        }

        internal static ServiceResponseModel<T> GetReferenceTypeModel<T>() where T : class
        {
            return new ServiceResponseModel<T>
            {
                Model = null,
                Messages = null
            };
        }

        internal static ServiceResponseModel<T> GetModel<T>(T model, params string[] errorMessages)
        {
            return new ServiceResponseModel<T>
            {
                Model = model,
                Messages = errorMessages?.ToList()
            };
        }

        internal static ServiceResponseModel<T> GetNullModelForReferenceType<T>(params string[] errorMessages) where T : class
        {
            return new ServiceResponseModel<T>
            {
                Model = null,
                Messages = errorMessages?.ToList()
            };
        }

        internal static ServiceResponseModel<T> GetNullModelForValueType<T>(params string[] errorMessages) where T : struct
        {
            return new ServiceResponseModel<T>
            {
                Model = default(T),
                Messages = errorMessages?.ToList()
            };
        }
    }
}



