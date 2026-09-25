public class Category
{
    public int CategoryID { get; set; }

    public string UID { get; set; } = Guid.NewGuid().ToString();
    public string CategoryName { get; set; }
    public ICollection<Product>? Products { get; set; } 
}
