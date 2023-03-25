using System.Globalization;

namespace dotnet7_new_features.Model
{
    public record Person (string Name, DateTime BirthDate, Person? Friend, bool? IsCitizen = true);
}
