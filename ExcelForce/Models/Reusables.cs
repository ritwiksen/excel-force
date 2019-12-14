using ExcelForce.Business.Interfaces;
using ExcelForce.Foundation.CoreServices.Models.Configuration;
using System.Collections.Generic;

namespace ExcelForce.Models
{
    public interface IInterface
    {
        string Test { get; set; }
    }

    public class Class : IInterface
    {
        public string Test { get; set; }
    }

    public class Service
    {
        public Service(IInterface test)
        {
            var object1 = test;
        }
    }



    public class Reusables
    {
        private static Reusables instance;

        public string ConnectionProfile { get; set; }

        public IEnumerable<string> FieldsForSearch { get; set; }

        public IEnumerable<string> SfObjects { get; set; }

        public ApiConfiguration ApiConfiguration { get; set; }

        public IExcelForceServiceFactory ExcelForceServiceFactory { get; set; }

        private Reusables() { }

        public static Reusables Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Reusables();
                }
                return instance;
            }
        }
    }
}
