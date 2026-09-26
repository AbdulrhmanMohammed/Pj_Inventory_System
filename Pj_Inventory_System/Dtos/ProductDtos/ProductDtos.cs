namespace Pj_Inventory_System.Dtos.ProductDtos
{
    // لإنشاء منتج جديد
    public class CreateProductDto
    {
        public string ProductName { get; set; }
        public int CategoryID { get; set; }
        public int SupplierID { get; set; }
        public int QuantityInStock { get; set; }
        public decimal UnitPrice { get; set; }
        public string? imageUrl { get; set; }
    }

    // للتعديل — يورّث من الإنشاء
    public class UpdateProductDto : CreateProductDto
    {
        public int ProductID { get; set; }
        public string? UID { get; set; }
    }

    // للعرض — يورّث من التعديل
    public class ProductDto : UpdateProductDto
    {
        public string? CategoryName { get; set; }
        public string? SupplierName { get; set; }
    }
}
