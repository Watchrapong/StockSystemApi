namespace StockSystem.Api.Domain;

public class ShoppingCart : BaseEntity
{
    public virtual ICollection<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();

    public DateTime? PaymentDateOnUtc { get; set; }
    public decimal TotalPrice { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
}