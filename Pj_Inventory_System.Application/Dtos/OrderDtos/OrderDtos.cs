namespace Pj_Inventory_System.Application.Dtos.OrderDtos
{
    public class CreateOrderDto
    {
        public int UserId { get; set; }

        public decimal TotalAmount { get; set; }
    }

    public class UpdateOrderDto : CreateOrderDto
    {
        public int OrderID { get; set; }

        public string UID { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;
    }

    public class OrderDto : UpdateOrderDto
    {
        public DateTime CreatedAt { get; set; }
    }
}