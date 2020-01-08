using ExcelForce.Foundation.CoreServices.Serialization.Interfaces;
using ExcelForce.Foundation.EntityManagement.Interfaces.ServiceInterfaces;
using ExcelForce.Foundation.EntityManagement.Models.ExtractMap;
using ExcelForce.Foundation.EntityManagement.Models.SfEntities;
using System;
using System.Linq;

namespace ExcelForce.Foundation.EntityManagement.Services
{
    public class ReadableExtractMapService : IReadableExtractMapService
    {
        private readonly IContentSerializationManager _contentSerializationManager;

        public ReadableExtractMapService(IContentSerializationManager contentSerializationManager)
        {
            _contentSerializationManager = contentSerializationManager;
        }

        public ReadableMapExtract GetContentFromQuery(SfQuery query)
        {
            if (query == null)
                throw new ArgumentNullException(nameof(query));

            var parentObject = query.GetParentObject();

            if (parentObject == null)
                throw new InvalidOperationException($"Parent object cannot be null for creating map");

            var mapExtract = new ReadableMapExtract
            {
                Parent = MapToReadableObject(parentObject),

                Children = query?.GetChildren()
                    ?.Select(x => MapToReadableObject(x))
                    ?.ToList()
            };

            return mapExtract;
        }

        private ReadableObject MapToReadableObject(SfObject source)
        {
            var output = new ReadableObject
            {
                ApiName = source.ApiName,

                Label = source.Name,

                SearchFilter = source.FilterExpressions,

                SortFilter = source.SortExpressions,

                Fields = source.Fields?.ToList()
            };

            return output;
        }
    }
}
