namespace StockSystem.Api.Response.Shoppingcart;

public class GetShoppingCartRes
{
    public Guid Id { get; set; }

    public List<GetShoppingCartResItem> Items { get; set; } = [];
}

public class GetShoppingCartResItem
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public decimal PricePerUnit { get; set; }
}