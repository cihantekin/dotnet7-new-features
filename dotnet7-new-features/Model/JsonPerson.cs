namespace dotnet7_new_features.Model
{
    public record JsonPerson (string Name, DateTime BirthDate, JsonPerson? Friend = null, bool? IsCitizen = true);
}
