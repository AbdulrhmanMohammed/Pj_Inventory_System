using Microsoft.EntityFrameworkCore;
using Pj_Inventory_System.Domain.Models;
using Pj_Inventory_System.Infrastructure.Data;

namespace Pj_Inventory_System.Infrastructure.Repositories.CartItemRepository
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly InventorySystemDbContext _context;

        public CartItemRepository(InventorySystemDbContext context)
        {
            _context = context;
        }

        public async Task<CartItem?> GetByIdAsync(int id)
        {
            return await _context.CartItems
                .Include(x => x.Product)   // مهم جداً
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<CartItem>> GetByUserAsync(int userId)
        {
            return await _context.CartItems
                .Include(x => x.Product)   // مهم جداً
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public async Task AddAsync(CartItem item)
        {
            await _context.CartItems.AddAsync(item);
        }

        public async Task UpdateAsync(CartItem item)
        {
            _context.CartItems.Update(item);
        }

        public async Task DeleteAsync(int id)
        {
            var item = await GetByIdAsync(id);
            if (item != null)
                _context.CartItems.Remove(item);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
