public class StockOut
{
    public int StockOutID { get; set; }

    public int ProductID { get; set; }
    public Product? Product { get; set; }

    public int Quantity { get; set; }
    public DateTime DateOut { get; set; }
}
