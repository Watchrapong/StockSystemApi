namespace StockSystem.Api.Response.Stock;

public class GetStockPagedRes : PagedRes
{
    public List<GetStockPagedResDataSource> DataSource { get; set; }
}

public class GetStockPagedResDataSource
{
    public Guid Id { get; set; }
    public string ProductCode { get; set; }
    public int Number { get; set; }
}