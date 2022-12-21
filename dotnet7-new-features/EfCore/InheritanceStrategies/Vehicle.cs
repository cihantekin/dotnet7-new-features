namespace dotnet7_new_features.EfCore.InheritanceStrategies
{
    // Default inheritance strategies : TPH (Table Per Hierarchy)
    //TPH maps an inheritance hierarchy of .NET types to a single database table.
    //
    //What is new? TPC (Table Per Concrete type)
    //in TPC, each table contains columns for every property in the concrete type and its base types
    //
    //For example, if Car, Bus, or Motorcycle have 20 properties for each type, then it's better to store them in a separate table, so we should use TPC. But if these entities have only 3-5 properties, then TPH is the way to go.

    public abstract class Vehicle
    {
        public int Id { get; set; }
        public string Model { get; set; } = new("");
    }

    public class Car : Vehicle
    {
        public string Segment { get; set; } = new("");
        public int DoorCount { get; set; }
    }

    public class Bus : Vehicle
    {
        public int SeatCount { get; set; }
    }
}
