using ExcelForce.Foundation.CoreServices.Models.Configuration;
using ExcelForce.Foundation.Persitence;

namespace ExcelForce.Foundation.Persistence.Persitence
{
    public interface IPersistenceContainer
    {
        IPersistenceManager<ApiConfiguration> ApiConfigurationManager { get; set; }

        T Get<T>(string key);

        bool Set<T>(string key, T value);

        bool Clear(string key);
    }
}
