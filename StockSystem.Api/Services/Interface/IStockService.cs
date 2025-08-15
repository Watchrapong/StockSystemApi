using StockSystem.Api.Response.Product;
using StockSystem.Api.Response.Stock;

namespace StockSystem.Api.Services.Interface;

public interface IStockService
{
    GetStockPagedRes GetStockPaged(int pageIndex = 0, int pageSize = 10, string? code = null, string? name = null);
}