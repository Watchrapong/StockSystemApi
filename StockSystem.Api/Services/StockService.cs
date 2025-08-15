using Microsoft.EntityFrameworkCore;
using StockSystem.Api.Data;
using StockSystem.Api.Domain;
using StockSystem.Api.Response.Product;
using StockSystem.Api.Response.Stock;
using StockSystem.Api.Services.Interface;

namespace StockSystem.Api.Services;

public class StockService : IStockService
{
    private readonly UnitOfWork _unitOfWork;

    public StockService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public GetStockPagedRes GetStockPaged(int pageIndex = 0, int pageSize = 10, string? code = null, string? name = null)
    {
        var stock = _unitOfWork.Repo<Stock>().GetAllPaged(x =>
        {
            x = x.Include(q => q.Product);

            if (!string.IsNullOrEmpty(code))
            {
                x = x.Where(p => p.Product.Code == code);
            }
            if (!string.IsNullOrEmpty(name))
            {
                x = x.Where(p => p.Product.Name == name);
            }
            
            return x;
        }, pageIndex, pageSize);

        var res = new GetStockPagedRes
        {
            PageIndex = stock.PageIndex,
            PageSize = stock.PageSize,
            HasNextPage = stock.HasNextPage,
            HasPreviousPage = stock.HasPreviousPage,
            TotalCount = stock.TotalCount,
            TotalPages = stock.TotalPages,
            DataSource = stock.Select(x => new GetStockPagedResDataSource()
            {
                Id = x.Id,
                Number = x.Number,
                ProductCode = x.Product.Code
            }).ToList()
        };

        return res;
    }
}