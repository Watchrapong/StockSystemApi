using Microsoft.AspNetCore.Mvc;
using StockSystem.Api.Request.ShoppingCart;
using StockSystem.Api.Services.Interface;

namespace StockSystem.Api.Controllers;

public class ShoppingCartController : BaseController
{
    private readonly IProductService _productService;
    private readonly IShoppingCartService _shoppingCartService;

    public ShoppingCartController(IProductService productService, IShoppingCartService shoppingCartService)
    {
        _productService = productService;
        _shoppingCartService = shoppingCartService;
    }

    [HttpGet("{userId}")]
    public IActionResult GetCart(Guid userId)
    {
        var cart = _shoppingCartService.GetCart(userId);
        return Success(cart);
    }
    
    [HttpPost]
    public IActionResult AddProduct(AddProductReq req)
    {
        _shoppingCartService.AddProduct(req);
        
        return Success();
    }
    
    [HttpPatch]
    public IActionResult RemoveProduct(AddProductReq req)
    {
        _shoppingCartService.RemoveProduct(req);
        
        return Success();
    }

    [HttpPut("{userId}")]
    public IActionResult ClearCart(Guid userId)
    {
        _shoppingCartService.ClearCart(userId);
        return Success();
    }

    [HttpPost("{userId}/checkout")]
    public IActionResult Checkout(Guid userId)
    { 
        _shoppingCartService.Checkout(userId);
        return Success();
    }
}