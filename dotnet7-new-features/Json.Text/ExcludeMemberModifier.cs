using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace dotnet7_new_features.Json.Text
{
    // Check for all: https://devblogs.microsoft.com/dotnet/system-text-json-in-dotnet-7/?utm_source=pocket_mylist
    public static class ExcludeMemberModifier
    {
        public static void ExcludeOldMember(JsonTypeInfo jsonTypeInfo)
        {
            if (jsonTypeInfo.Kind != JsonTypeInfoKind.Object)
            {
                return;
            }

            foreach (var prop in jsonTypeInfo.Properties)
            {
                // if property name ends with Old when you convert the model to json, that property will be ignored.
                if (prop.Name.EndsWith("Old"))
                {
                    prop.ShouldSerialize = static (obj, value) => false;
                }
            }
        }

    }

    public class ExcludeMemberTestClass
    {
        public string StringProp { get; set; } = new("");
        public string StringPropOld { get; set; } = new("");
    }

    public class Person
    {
        // It should be noted that required properties are currently not supported by the source generator.
        [JsonRequired]
        public required string Name { get; set; }
        public int Age { get; set; }
    }
}
