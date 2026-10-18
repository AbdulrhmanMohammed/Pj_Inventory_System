using Pj_Inventory_System.Domain.Models;

namespace Pj_Inventory_System.Infrastructure.Repositories.CartItemRepository
{
    public interface ICartItemRepository
    {
        Task<CartItem?> GetByIdAsync(int id);
        Task<List<CartItem>> GetByUserAsync(int userId);
        Task AddAsync(CartItem item);
        Task UpdateAsync(CartItem item);
        Task DeleteAsync(int id);
        Task SaveAsync();
    }
}
