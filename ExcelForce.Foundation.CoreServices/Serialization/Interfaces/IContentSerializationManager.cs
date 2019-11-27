namespace ExcelForce.Foundation.CoreServices.Serialization.Interfaces
{
    public interface IContentSerializationManager
    {
        /// <summary>
        /// This method would take care of Deserialization of an object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <returns></returns>
        T Deserialize<T>(string content);

        /// <summary>
        /// This method would take care of Serializing an object to a string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        string Serialize<T>(T obj);
    }
}
