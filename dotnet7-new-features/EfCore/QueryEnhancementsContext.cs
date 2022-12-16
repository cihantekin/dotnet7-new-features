using dotnet7_new_features.EfCore;
using dotnet7_new_features.EfCore.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace dotnet7_new_features.Queries
{
    public class QueryEnhancementsContext : DbContext
    {
        public QueryEnhancementsContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(new MyDbContextInterceptor());
        }

        public DbSet<Product> Products { get; set; }
    }
}