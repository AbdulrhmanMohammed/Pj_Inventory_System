using Pj_Inventory_System.Domain.Models;

namespace Pj_Inventory_System.Infrastructure.Repositories
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllAsync();

        Task<List<Order>> GetOrdersByUser(int userId);

        Task<List<CartItem>> GetUserCart(int userId);

        Task<Order?> GetByIdAsync(int id);

        Task AddAsync(Order order);

        Task UpdateAsync(Order order);

        Task DeleteAsync(Order order);

        Task DeleteCartItems(List<CartItem> cartItems);

        Task UpdateProduct(Product product);

        Task SaveChangesAsync();
    }
}