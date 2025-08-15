using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockSystem.Api.Domain;

namespace StockSystem.Api.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasData(new List<User>()
        {
            new()
            {
                Id = Guid.NewGuid(),
                Firstname = "John",
                Lastname = "Doe",
            }
        });
    }
}