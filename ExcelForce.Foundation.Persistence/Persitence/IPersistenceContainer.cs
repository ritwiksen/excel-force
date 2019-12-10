using ExcelForce.Foundation.Persitence;
using System.Collections.Generic;

namespace ExcelForce.Foundation.Persistence.Persitence
{
    public interface IPersistenceContainer
    {
        IPersistenceManager<IEnumerable<string>> SfObjectsManager { get; set; }

        IPersistenceManager<IEnumerable<string>> SfAttributesManager { get; set; }
    }
}
