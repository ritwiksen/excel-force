using System;
using ExcelForce.Foundation.EntityManagement.Models.ExtractMap;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace ExcelForce.Foundation.EntityManagement.Infrastructure.CustomSerializers
{
    public class SfDisplayDataSerializer : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(SfExtractDataWrapper);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.StartObject)
                return null;

            var item = JObject.Load(reader);

            var records = GetRecords(item);

            return records;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        private static SfExtractDataWrapper GetRecords(JObject jObj)
        {

            var result = new SfExtractDataWrapper();

            var records = jObj["records"];

            if (records == null)
                return null;

            foreach (var record in records)
            {
                var data = GetDataForRecord(record);

                result.ObjectName = data.Item1;

                result.Data.Add(data.Item2);
            }

            return result;
        }

        private static Tuple<string, SfExtractDataModel> GetDataForRecord(JToken record)
        {
            var listData = new SfExtractDataModel();

            var type = string.Empty;

            foreach (JProperty x in record)
            {
                string name = x.Name;

                if (string.Equals(name, "attributes"))
                {
                    listData.Url = x.Value["url"].ToString()?.Trim();
                    type = x.Value["type"].ToString()?.Trim();
                    continue;
                }

                var value = x.Value;

                var result = value.IsValid(GetJsonSchema());

                if (result)
                {
                    var complexObject = JObject.Parse(value?.ToString());

                    listData.Data.Add(x.Name, GetRecords(complexObject));
                }
                else
                    listData.Data.Add(x.Name, value?.ToString());
            }

            return Tuple.Create(type, listData);
        }

        private static JsonSchema GetJsonSchema() => JsonSchema.Parse(
            EntityManagementConstants.SfEntitySchema);
    }
}
