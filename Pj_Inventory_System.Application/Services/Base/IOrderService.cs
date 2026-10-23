using Pj_Inventory_System.Application.Dtos.OrderDtos;
using Pj_Inventory_System.Domain.Models;

namespace Pj_Inventory_System.Application.Services.Base
{
    public interface IOrderService
    {
        Task<List<OrderDto>> GetAllAsync();

        Task<OrderDto?> GetByIdAsync(int id);

        Task CreateAsync(CreateOrderDto dto);

        Task UpdateAsync(UpdateOrderDto dto);

        Task DeleteAsync(int id);

        Task<int> MakeOrder(int userId);

        Task<List<Order>> GetMyOrders(int userId);




    }
}
