public class Supplier
{
    public int SupplierID { get; set; }

    public string UID { get; set; } = Guid.NewGuid().ToString();
    public string SupplierName { get; set; }
    public ICollection<Product>? Products { get; set; }
}
