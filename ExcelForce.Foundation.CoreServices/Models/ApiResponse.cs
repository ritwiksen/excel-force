using System.Net;

namespace ExcelForce.Foundation.CoreServices.Models
{
    public class ApiResponse<TModel, TErrorResponse>
    {
        public TModel Model { get; set; }

        public TErrorResponse Error { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public bool IsValid(bool allowNullResponse)
        {
            if (allowNullResponse)
                return Error == null;
            
             return Model != null && Error == null && (int)StatusCode ==200;
        }

    }
}
