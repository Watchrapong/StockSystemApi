using Microsoft.AspNetCore.Mvc;
using StockSystem.Api.Services.Interface;

namespace StockSystem.Api.Controllers;

public class StockController : BaseController
{
    private readonly IStockService _stockService;

    public StockController(IStockService stockService)
    {
        _stockService = stockService;
    }
    
    [HttpGet]
    public IActionResult GetPaged([FromQuery]int pageIndex = 0, int pageSize = 10,  string? code = null, string? name = null)
    {
        var stocks = _stockService.GetStockPaged(pageIndex, pageSize, code, name);
        
        return Success(stocks);
    }
}