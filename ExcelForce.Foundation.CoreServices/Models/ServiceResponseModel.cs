using System.Collections.Generic;

namespace ExcelForce.Foundation.CoreServices.Models
{
    public class ServiceResponseModel<T>
    {
        public T Model { get; set; }

        public List<string> Messages { get; set; }
    }
}
