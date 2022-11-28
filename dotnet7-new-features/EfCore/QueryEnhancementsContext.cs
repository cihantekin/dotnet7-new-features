using Microsoft.EntityFrameworkCore;

namespace dotnet7_new_features.Queries
{
    public class QueryEnhancementsContext : DbContext
    {
        public QueryEnhancementsContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}