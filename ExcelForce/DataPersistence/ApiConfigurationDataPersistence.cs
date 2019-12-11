using ExcelForce.Foundation.CoreServices.Models.Configuration;
using ExcelForce.Foundation.Persitence;
using ExcelForce.Models;
using System;

namespace ExcelForce.DataPersistence
{
    public class ApiConfigurationDataPersistence : IPersistenceManager<ApiConfiguration>
    {
        public ApiConfigurationDataPersistence()
        {

        }

        public ApiConfigurationDataPersistence(string productionUrl, string sandboxUrl)
        {
            var apiConfiguration = new ApiConfiguration
            {
                LocalUrl = sandboxUrl,
                ProductionUrl = productionUrl
            };

            Set(apiConfiguration);
        }

        public bool Clear()
        {
            return Set(null);
        }

        public ApiConfiguration Get()
        {
            return Reusables.Instance.ApiConfiguration;
        }

        public bool Set(ApiConfiguration persitenceObject)
        {
            try
            {
                Reusables.Instance.ApiConfiguration = persitenceObject;

                return true;
            }
            catch (Exception ex)
            {
                //Implement logging here
                //(Ritwik)TODO
                return false;
            }
        }
    }
}
