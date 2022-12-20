using dotnet7_new_features.EfCore.CustomConventions;
using dotnet7_new_features.EfCore.InheritanceStrategies;
using dotnet7_new_features.EfCore.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

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

        // Remove this before updating the database and see the difference on the tables.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>().UseTpcMappingStrategy();

            modelBuilder.Entity<Product>().OwnsOne(product => product.ProductDetail, builder =>
            {
                builder.ToJson();
                builder.OwnsOne(pd => pd.ProducerAddress);
            });
        }

        // You can remove/replace/add convention
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Conventions.Remove(typeof(ForeignKeyIndexConvention));
            configurationBuilder.Conventions.Add(_ => new DecimalPrecisionInvention());
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Bus> Buses { get; set; }
    }
}