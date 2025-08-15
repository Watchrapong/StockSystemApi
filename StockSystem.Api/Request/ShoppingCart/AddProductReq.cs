using System.ComponentModel.DataAnnotations;

namespace StockSystem.Api.Request.ShoppingCart;

public class AddProductReq
{
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public Guid ProductId { get; set; }
    [Required]
    public int Quantity { get; set; }
}