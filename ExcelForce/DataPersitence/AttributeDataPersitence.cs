using ExcelForce.Foundation.CoreServices.Persitence;
using ExcelForce.Foundation.EntityManagement.Models.SfEntities;
using ExcelForce.Models;
using System;
using System.Collections.Generic;

namespace ExcelForce.DataPersitence
{
    public class AttributeDataPersitence : IPersistenceManager<IEnumerable<string>>
    {
        public bool Clear()
        {
            return Set(null);
        }

        public IEnumerable<string> Get()
        {
            return Reusables.Instance.FieldsForSearch;
        }

        public bool Set(IEnumerable<string> persitenceObject)
        {
            try
            {
                Reusables.Instance.FieldsForSearch = persitenceObject;

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
