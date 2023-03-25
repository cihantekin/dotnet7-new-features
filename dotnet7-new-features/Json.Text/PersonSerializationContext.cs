using System.Text.Json.Serialization;

namespace dotnet7_new_features.Json.Text
{
    [JsonSourceGenerationOptions(WriteIndented = true, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
    [JsonSerializable(typeof(Person))]
    public partial class PersonSerializationContext : JsonSerializerContext { }
}
