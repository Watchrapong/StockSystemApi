using StockSystem.Api.Request.ShoppingCart;

namespace StockSystem.Api.Services.Interface;

public interface IShoppingCartService
{
    object GetCart(Guid userId);
    void AddProduct(AddProductReq req);
    void RemoveProduct(AddProductReq req);
    void ClearCart(Guid userId);
    void Checkout(Guid userId);
}