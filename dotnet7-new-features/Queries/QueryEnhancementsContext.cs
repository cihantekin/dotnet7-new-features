using Microsoft.EntityFrameworkCore;

namespace dotnet7_new_features.Queries
{
    internal class QueryEnhancementsContext : DbContext
    {
        public QueryEnhancementsContext()
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}