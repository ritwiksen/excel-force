using ExcelForce.Foundation.CoreServices.Models.Configuration;
using ExcelForce.Foundation.Persistence.Persitence;
using ExcelForce.Foundation.Persitence;
using System;
using System.Collections.Generic;

namespace ExcelForce.Infrastructure.DataPersistence
{
    public class ExcelForcePersistenceContainer : IPersistenceContainer
    {
        public IPersistenceManager<ApiConfiguration> ApiConfigurationManager { get; set; }

        private Dictionary<string, object> _containerValue;

        public T GetPersistence<T>(string key)
        {
            _containerValue = _containerValue ?? new Dictionary<string, object>();

            return _containerValue.ContainsKey(key) ? (T)_containerValue[key] : default(T);
        }

        public bool SetPersistence<T>(string key, T value)
        {
            try
            {
                _containerValue = _containerValue ?? new Dictionary<string, object>();

                if (_containerValue.ContainsKey(key))
                {
                    _containerValue[key] = value;
                }
                else
                {
                    _containerValue.Add(key, value);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
