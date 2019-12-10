using ExcelForce.Foundation.Persistence.Persitence;
using ExcelForce.Foundation.Persitence;
using System.Collections.Generic;

namespace ExcelForce.DataPersistence
{
    public class ExcelForcePersistenceContainer : IPersistenceContainer
    {
        public IPersistenceManager<IEnumerable<string>> SfObjectsManager { get; set; }
        public IPersistenceManager<IEnumerable<string>> SfAttributesManager { get; set; }
    }
}
