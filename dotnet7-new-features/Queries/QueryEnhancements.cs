using Microsoft.EntityFrameworkCore;

namespace dotnet7_new_features.Queries
{
    public class QueryEnhancements
    {
        private readonly QueryEnhancementsContext _ctx;

        public QueryEnhancements(QueryEnhancementsContext ctx)
        {
            _ctx = ctx;
        }

        public void Test()
        {
            // now groupby is a final operator. previously the expression could not be translated.
            // grouping will be with  returned results
            var queryString = _ctx.Products.GroupBy(x => x.Price).ToQueryString();
            Console.WriteLine(queryString);
        }

        public async Task BulkDelete()
        {
            // no change tracker
            await _ctx.Products.Where(x => x.Name.Contains("test")).ExecuteDeleteAsync();

            //
            //DELETE FROM[p]
            //FROM[Products] AS[p]
            //WHERE[p].[Name] LIKE N'%test%'
        }

        public async Task BulkUpdate()
        {
            await _ctx.Products.ExecuteUpdateAsync(s => s.SetProperty(b => b.Name, b => b.Name + "test"));
        }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
