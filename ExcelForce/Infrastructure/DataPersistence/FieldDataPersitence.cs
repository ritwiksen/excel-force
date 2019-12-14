using ExcelForce.Foundation.Persitence;
using ExcelForce.Models;
using System;
using System.Collections.Generic;

namespace ExcelForce.Infrastructure.DataPersitence
{
    public class FieldDataPersitence : IPersistenceManager<IEnumerable<string>>
    {
        public bool Clear()
        {
            return Set(null);
        }

        public IEnumerable<string> Get()
        {
            return Reusables.Instance.SfObjects;
        }

        public bool Set(IEnumerable<string> persitenceObject)
        {
            try
            {
                Reusables.Instance.SfObjects = persitenceObject;

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
