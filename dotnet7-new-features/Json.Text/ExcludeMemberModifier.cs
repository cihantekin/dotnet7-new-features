using System.Text.Json.Serialization.Metadata;

namespace dotnet7_new_features.Json.Text
{
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
        public string StringProp { get; set; }
        public string StringPropOld { get; set; }
    }
}
