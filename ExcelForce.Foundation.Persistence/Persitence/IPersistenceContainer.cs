using ExcelForce.Foundation.CoreServices.Models.Configuration;
using ExcelForce.Foundation.Persitence;

namespace ExcelForce.Foundation.Persistence.Persitence
{
    public interface IPersistenceContainer
    {
        IPersistenceManager<ApiConfiguration> ApiConfigurationManager { get; set; }

        T GetPersistence<T>(string key);

        bool SetPersistence<T>(string key, T value);
    }
}
