namespace Pj_Inventory_System.Dtos.SupplierDtos
{
    // لإنشاء مورد جديد
    public class CreateSupplierDto
    {
        public string SupplierName { get; set; }
    }

    // للتعديل — يورّث من الإنشاء
    public class UpdateSupplierDto : CreateSupplierDto
    {
        public int SupplierID { get; set; }
        public string UID { get; set; }
    }

    // للعرض — يورّث من التعديل
    public class SupplierDto : UpdateSupplierDto
    {
        public int? ProductsCount { get; set; }
    }
}
