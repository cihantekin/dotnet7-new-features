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
            // this query will be detected by TaggedQueryCommandInterceptor
            await _ctx.Products.TagWith("Use hint: robust plan").Where(x => x.Name.Contains("test")).ExecuteDeleteAsync();

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
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public ProductDetail ProductDetail { get; set; }
    }

    public class ProductDetail
    {
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public string UserManual { get; set; }
        public DateOnly ProductionYear { get; set; }
        public Address ProducerAddress { get; set; }
    }

    public class Address
    {
        public Address(string street, string city, string postcode, string country)
        {
            Street = street;
            City = city;
            Postcode = postcode;
            Country = country;
        }

        public string Street { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
        public string Country { get; set; }
    }
}
