using dotnet7_new_features.EfCore;
using dotnet7_new_features.EfCore.InheritanceStrategies;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>().UseTpcMappingStrategy();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Bus> Buses { get; set; }
    }
}