using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockSystem.Api.Domain;

namespace StockSystem.Api.Configuration;

public class StockConfiguration :  IEntityTypeConfiguration<Stock>
{
    public void Configure(EntityTypeBuilder<Stock> builder)
    {
        
        
    }
}