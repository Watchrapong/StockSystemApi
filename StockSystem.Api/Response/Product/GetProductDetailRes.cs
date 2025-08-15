namespace StockSystem.Api.Response.Product;

public class GetProductDetailRes
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public decimal PricePerUnit { get; set; }
}