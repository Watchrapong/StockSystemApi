namespace StockSystem.Api.Domain;

public class ShoppingCartItem: BaseEntity
{
    public Guid ProductId { get; set; }
    public virtual Product Product { get; set; }
    public int Quantity { get; set; }
    public Guid ShoppingCartId { get; set; }
    public virtual ShoppingCart ShoppingCart { get; set; }
    public decimal PricePerUnit { get; set; }
}