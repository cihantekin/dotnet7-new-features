using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace dotnet7_new_features.EfCore
{
    public class MaxStringLengthConvention : IModelFinalizingConvention
    {
        public void ProcessModelFinalizing(IConventionModelBuilder modelBuilder, IConventionContext<IConventionModelBuilder> context)
        {
            foreach (var property in modelBuilder.Metadata.GetEntityTypes().SelectMany(e => e.GetDeclaredProperties().Where(prop => prop.ClrType == typeof(string))))
            {
                property.Builder.HasMaxLength(512);
            }
        }
    }
}
