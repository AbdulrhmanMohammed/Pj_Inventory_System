namespace Pj_Inventory_System.Dtos.StockInDtos
{
    // لإنشاء عملية إدخال مخزون جديدة
    public class CreateStockInDto
    {
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public DateTime DateIn { get; set; }
    }

    // للتعديل — يورّث من الإنشاء
    public class UpdateStockInDto : CreateStockInDto
    {
        public int StockInID { get; set; }
        public string UID { get; set; }
    }

    // للعرض — يورّث من التعديل
    public class StockInDto : UpdateStockInDto
    {
        public string? ProductName { get; set; }
    }
}
