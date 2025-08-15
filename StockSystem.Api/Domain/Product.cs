namespace StockSystem.Api.Domain;

public class Product : BaseEntity
{
    /// <summary>
    /// รหัสสินค้า
    /// </summary>
    public string Code { get; set; }
    /// <summary>
    /// ชื่อสินค้า
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// ราคาต่อหน่วย
    /// </summary>
    public decimal PricePerUnit { get; set; }
}