using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace Conversions.Converters
{
    internal class JsonConverter<T> : ValueConverter<T, string>
    {
        public JsonConverter()
            : this(null)
        {

        }

        public JsonConverter(JsonSerializerOptions options) 
            : base
                  (
                     item => JsonSerializer.Serialize(item, options),
                     value => JsonSerializer.Deserialize<T>(value, options)
                  )
        {

        }
    }
}
