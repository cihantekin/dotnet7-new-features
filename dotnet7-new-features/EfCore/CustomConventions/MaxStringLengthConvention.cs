using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace dotnet7_new_features.EfCore.CustomConventions
{
    public class MaxStringLengthConvention : IModelFinalizingConvention
    {
        public void ProcessModelFinalizing(IConventionModelBuilder modelBuilder, IConventionContext<IConventionModelBuilder> context)
        {
            foreach (var property in modelBuilder.Metadata.GetEntityTypes().SelectMany(e => e.GetDeclaredProperties().Where(prop => prop.ClrType == typeof(string))))
            {
                // If there is an exception for that rule go and mark that property with [MaxLength] attribute
                property.Builder.HasMaxLength(512);
            }
        }
    }
}
