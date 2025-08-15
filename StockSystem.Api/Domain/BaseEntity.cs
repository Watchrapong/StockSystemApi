using StockSystem.Api.Utils;

namespace StockSystem.Api.Domain;

public abstract class BaseEntity : ISoftDelete
{
    public Guid Id { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public DateTime UpdatedOnUtc { get; set; }
    public DateTime? DeletedOnUtc { get; set; }
}