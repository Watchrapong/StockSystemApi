namespace StockSystem.Api.Utils;

public interface ISoftDelete
{
    DateTime? DeletedOnUtc  { get; set; }
}