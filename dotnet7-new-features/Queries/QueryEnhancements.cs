namespace dotnet7_new_features.Queries
{
    public class QueryEnhancements
    {
        public QueryEnhancements()
        {
        using var ctx = new QueryEnhancementsContext();

        }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
