namespace Pj_Inventory_System.Application.Dtos.CartItemDtos
{
    // لإنشاء عنصر جديد في السلة
    public class CreateCartItemDto
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    // للتعديل على عنصر موجود
    public class UpdateCartItemDto : CreateCartItemDto
    {
        public int CartItemID { get; set; }
        public string UID { get; set; } // مهم جدا للفيو والكنترولر
    }

    // للعرض في الفيو أو API
    public class CartItemDto : UpdateCartItemDto
    {
    }
}
