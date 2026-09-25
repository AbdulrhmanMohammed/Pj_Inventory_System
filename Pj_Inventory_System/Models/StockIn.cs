public class StockIn
{
    public int StockInID { get; set; }

    public string UID { get; set; } = Guid.NewGuid().ToString();
    public int ProductID { get; set; }
    public Product? Product { get; set; }

    public int Quantity { get; set; }
    public DateTime DateIn { get; set; }
}
