namespace StockSystem.Api.Response.Product;

public class GetProductPagedRes : PagedRes
{
    public List<GetProductPagedResDataSource> DataSource { get; set; }
}

public class GetProductPagedResDataSource
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public decimal PricePerUnit { get; set; }
}