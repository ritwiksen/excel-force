using ExcelForce.Foundation.CoreServices.Serialization.Interfaces;
using Newtonsoft.Json;

namespace ExcelForce.Foundation.CoreServices.Serialization
{
    public class JsonSerializer : IContentSerializationManager
    {
        public T Deserialize<T>(string content)
        {
            var result
                = JsonConvert.DeserializeObject<T>(content, GetSerializerSettings());

            return result;
        }

        public string Serialize<T>(T obj)
        {
            var result
                = JsonConvert.SerializeObject(obj, GetSerializerSettings());

            return result;
        }

        private static JsonSerializerSettings GetSerializerSettings() => new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        };
    }
}
