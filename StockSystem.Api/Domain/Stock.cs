namespace StockSystem.Api.Domain;

public class Stock: BaseEntity
{
    public Guid ProductId { get; set; }
    public virtual Product Product { get; set; }
    /// <summary>
    /// จำนวนสินค้่าใน stock
    /// </summary>
    private int _Number;
    public int Number
    {
        get { return _Number; }
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException("Number", "ต้องมีค่าไม่น้อยกว่า 0");

            _Number = value;
        }
    }
}