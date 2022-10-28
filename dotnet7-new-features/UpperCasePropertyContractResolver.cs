using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace dotnet7_new_features
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
                    prop.Name = prop.Name.ToUpperInvariant();
                }
            }

            return info;
        }

        public string SerializeMyModel()
        {
            var options = new JsonSerializerOptions { TypeInfoResolver = new UpperCasePropertyContractResolver() };
            return JsonSerializer.Serialize(new { value = "uppercase test" }, options);
        }
    }
}
