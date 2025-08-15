using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockSystem.Api.Domain;

namespace StockSystem.Api.Configuration;

public class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
{
    public void Configure(EntityTypeBuilder<ShoppingCart> builder)
    {
        builder.HasMany(sc => sc.Items)
               .WithOne(sci => sci.ShoppingCart)
               .HasForeignKey(sc => sc.ShoppingCartId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}