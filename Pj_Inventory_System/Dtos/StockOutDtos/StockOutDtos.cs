namespace Pj_Inventory_System.Dtos.StockOutDtos
{
    // لإنشاء عملية إخراج مخزون جديدة
    public class CreateStockOutDto
    {
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public DateTime DateOut { get; set; }
    }

    // للتعديل — يورّث من الإنشاء
    public class UpdateStockOutDto : CreateStockOutDto
    {
        public int StockOutID { get; set; }
        public string UID { get; set; }
    }

    // للعرض — يورّث من التعديل
    public class StockOutDto : UpdateStockOutDto
    {
        public string? ProductName { get; set; }
    }
}
