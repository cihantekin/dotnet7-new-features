using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace dotnet7_new_features.EfCore.CustomConventions
{
    public class DecimalPrecisionInvention : IModelFinalizingConvention
    {
        public void ProcessModelFinalizing(IConventionModelBuilder modelBuilder, IConventionContext<IConventionModelBuilder> context)
        {
            foreach (var property in modelBuilder.Metadata.GetEntityTypes().SelectMany(x => x.GetDeclaredProperties().Where(y => y.ClrType == typeof(decimal))))
            {
                property.Builder.HasPrecision(2);
            }
        }
    }
}
