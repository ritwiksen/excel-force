using ExcelForce.Foundation.CoreServices.Models.Interfaces;
using System.Collections.Generic;

namespace ExcelForce.Foundation.CoreServices.Repository
{
    public interface IExcelForceRepository<T, S> where T : IExcelForceModel
    {
        /// <summary>
        /// Fetches all records for the model type T
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetRecords();

        /// <summary>
        /// Adds a model of type T to a collection
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddRecord(T model);

        /// <summary>
        /// Updates a record of model T with an identifying key S
        /// </summary>
        /// <param name="key"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateRecord(S key, T model);

        /// <summary>
        /// Deletes a record from the collection with the help of an identifier
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool DeleteRecord(S key);

        /// <summary>
        /// Deletes a record from the collection with the help of an identifier
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool DeleteRecordByMapNameAndKey(S Key,S key);
    }
}
