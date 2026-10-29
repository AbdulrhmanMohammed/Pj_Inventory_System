using Microsoft.EntityFrameworkCore;
using Pj_Inventory_System.Domain.Models;
using Pj_Inventory_System.Infrastructure.Data;

namespace Pj_Inventory_System.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly InventorySystemDbContext _context;

        public OrderRepository(
            InventorySystemDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await _context.Orders
                .Include(x => x.OrderItems)
                .ThenInclude(x => x.Product)
                .ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _context.Orders
                .Include(x => x.OrderItems)
                .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Order>> GetOrdersByUser(
            int userId)
        {
            return await _context.Orders
                .Include(x => x.OrderItems)
                .ThenInclude(x => x.Product)
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public async Task<List<CartItem>> GetUserCart(
            int userId)
        {
            return await _context.CartItems
                .Include(x => x.Product)
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
        }

        public async Task UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
        }

        public async Task DeleteAsync(Order order)
        {
            _context.Orders.Remove(order);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCartItems(List<CartItem> cartItems)
        {
            _context.CartItems.RemoveRange(cartItems);
            await Task.CompletedTask;
        }

        public async Task UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            await Task.CompletedTask;
        }

    }
}