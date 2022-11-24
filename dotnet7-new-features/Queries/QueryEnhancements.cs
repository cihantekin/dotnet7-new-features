using Microsoft.EntityFrameworkCore;

namespace dotnet7_new_features.Queries
{
    public class QueryEnhancements
    {
        public QueryEnhancements()
        {
            using var ctx = new QueryEnhancementsContext();
            // now groupby is a final operator. previously the expression could not be translated.
            // grouping will be with  returned results
            var queryString = ctx.Products.GroupBy(x => x.Price).ToQueryString();
            Console.WriteLine(queryString);
        }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
