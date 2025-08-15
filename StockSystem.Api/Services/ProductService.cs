using StockSystem.Api.Data;
using StockSystem.Api.Domain;
using StockSystem.Api.Response.Product;
using StockSystem.Api.Services.Interface;

namespace StockSystem.Api.Services;

public class ProductService : IProductService
{
    private readonly UnitOfWork _unitOfWork;

    public ProductService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public GetProductPagedRes GetProductPaged(int pageIndex = 0, int pageSize = 10, string? code = null, string? name = null)
    {
        var product = _unitOfWork.Repo<Product>()
                                 .GetAllPaged(x =>
                                 {
                                     if (!string.IsNullOrEmpty(code))
                                     {
                                         x = x.Where(p => p.Code.ToLower().Contains(code.Trim().ToLower()));
                                     }
                                     if (!string.IsNullOrEmpty(name))
                                     {
                                         x = x.Where(p => p.Name.ToLower().Contains(name.Trim().ToLower()));
                                     }
                                     return x;
                                 }, pageIndex, pageSize);

        var res = new GetProductPagedRes
        {
            PageIndex = product.PageIndex,
            PageSize = product.PageSize,
            HasNextPage = product.HasNextPage,
            HasPreviousPage = product.HasPreviousPage,
            TotalCount = product.TotalCount,
            TotalPages = product.TotalPages,
            DataSource = product.Select(x => new GetProductPagedResDataSource
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                PricePerUnit = x.PricePerUnit,
            }).ToList()
        };

        return res;
    }

    public GetProductDetailRes GetProductDetail(Guid id)
    {
        var product = _unitOfWork.Repo<Product>().Table
                                 .FirstOrDefault(x => x.Id == id);

        if (product == null)
        {
            throw new ApplicationException("Product not found");
        }

        return new()
        {
            Id = product.Id,
            Code = product.Code,
            Name = product.Name,
            PricePerUnit = product.PricePerUnit,
        };
    }
    
    #region Mock

    public void MockProduct()
    {
        var row = 1000;
        var now = DateTime.UtcNow; 
        var random = new Random();
        
        for (int i = 0; i < row; i++)
        {
            var product = new Product()
            {
                Code = $"P{now.Second}{now.Millisecond}{i+1}",
                Name = $"Product {i + 1}",
                PricePerUnit = random.Next(100, 5000),
            };
            _unitOfWork.Repo<Product>().Insert(product);
            var stock = new Stock()
            {
                ProductId = product.Id,
                Number = random.Next(0, 500),
            };
            
            _unitOfWork.Repo<Stock>().Insert(stock);
            _unitOfWork.Save();
        }
        
    }
    #endregion
}