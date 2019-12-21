using System.Collections.Generic;

namespace ExcelForce.Foundation.CoreServices.Models
{
    public class ServiceResponseModel<T>
    {

        public T Model { get; set; }

        public List<string> Messages { get; set; }

        public bool IsValid() => (
            ((!typeof(T).GetType().IsValueType && Model != null) || (typeof(T).GetType().IsValueType))
                && (Messages?.Count ?? 0) == 0);
    }
}
