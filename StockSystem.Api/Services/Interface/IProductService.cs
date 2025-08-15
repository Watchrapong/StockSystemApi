using StockSystem.Api.Response.Product;

namespace StockSystem.Api.Services.Interface;

public interface IProductService
{
    GetProductPagedRes GetProductPaged(int pageIndex = 0, int pageSize = 10,  string? productCode = null, string? productName = null);
    
    GetProductDetailRes GetProductDetail(Guid id);

    #region Mock

    void MockProduct();

    #endregion
}