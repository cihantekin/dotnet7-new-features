using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace dotnet7_new_features.Json.Text
{
    public class UpperCasePropertyContractResolver : DefaultJsonTypeInfoResolver
    {
        public override JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options)
        {
            JsonTypeInfo info = base.GetTypeInfo(type, options);

            if (info.Kind == JsonTypeInfoKind.Object)
            {
                foreach (var prop in info.Properties)
                {
                    // Turn prop names to uppercase 
                    prop.Name = prop.Name.ToUpperInvariant();
                }
            }

            return info;
        }
    }
}
