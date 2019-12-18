using ExcelForce.Foundation.CoreServices.Models.Configuration;
using ExcelForce.Foundation.Persitence;
using System.Collections.Generic;

namespace ExcelForce.Foundation.Persistence.Persitence
{
    public interface IPersistenceContainer
    {
        IPersistenceManager<IEnumerable<string>> SfObjectsManager { get; set; }

        IPersistenceManager<IEnumerable<string>> SfAttributesManager { get; set; }

        IPersistenceManager<ApiConfiguration> ApiConfigurationManager { get; set; }

        T GetPersistence<T>(string key);

        bool SetPersistence<T>(string key, T value);
    }
}
