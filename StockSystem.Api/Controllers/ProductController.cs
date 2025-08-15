using Microsoft.AspNetCore.Mvc;
using StockSystem.Api.Services.Interface;

namespace StockSystem.Api.Controllers;

public class ProductController : BaseController
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
    
    [HttpGet]
    public IActionResult GetPaged([FromQuery]int pageIndex = 0, int pageSize = 10, string? code = null, string? name = null)
    {
        var products = _productService.GetProductPaged(pageIndex, pageSize, code, name);
        
        return Success(products);
    }

    [HttpGet("mock")]
    public IActionResult Mock()
    {
        _productService.MockProduct();

        return Success();
    }
}