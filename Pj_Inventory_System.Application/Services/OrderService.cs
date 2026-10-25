using Pj_Inventory_System.Application.Dtos.OrderDtos;
using Pj_Inventory_System.Application.Services.Base;
using Pj_Inventory_System.Domain.Models;
using Pj_Inventory_System.Infrastructure.Repositories;

namespace Pj_Inventory_System.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repo;

        public OrderService(
            IOrderRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<OrderDto>> GetAllAsync()
        {
            var orders = await _repo.GetAllAsync();

            return orders.Select(x => new OrderDto
            {
                OrderID = x.Id,
                UserId = x.UserId,
                Status = x.Status,
                TotalAmount = x.TotalAmount,
                CreatedAt = x.CreatedAt,
                UID = x.Id.ToString()
            }).ToList();
        }

        public async Task<OrderDto?> GetByIdAsync(int id)
        {
            var order = await _repo.GetByIdAsync(id);

            if (order == null)
                return null;

            return new OrderDto
            {
                OrderID = order.Id,
                UserId = order.UserId,
                Status = order.Status,
                TotalAmount = order.TotalAmount,
                CreatedAt = order.CreatedAt,
                UID = order.Id.ToString()
            };
        }

        public async Task CreateAsync(CreateOrderDto dto)
        {
            var order = new Order
            {
                UserId = dto.UserId,
                TotalAmount = dto.TotalAmount
            };

            await _repo.AddAsync(order);

            await _repo.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateOrderDto dto)
        {
            var order = await _repo.GetByIdAsync(dto.OrderID);

            if (order == null)
                return;

            order.Status = dto.Status;
            order.TotalAmount = dto.TotalAmount;

            await _repo.UpdateAsync(order);

            await _repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var order = await _repo.GetByIdAsync(id);

            if (order == null)
                return;

            await _repo.DeleteAsync(order);

            await _repo.SaveChangesAsync();
        }

        public async Task<List<Order>> GetMyOrders(int userId)
        {
            return await _repo.GetOrdersByUser(userId);
        }

        public async Task<int> MakeOrder(int userId)
        {
            var cartItems = await _repo.GetUserCart(userId);

            if (!cartItems.Any())
                return 0;

            var order = new Order
            {
                UserId = userId,
                Status = "Pending",
                CreatedAt = DateTime.UtcNow,
                TotalAmount = cartItems.Sum(x =>
                    x.Quantity * x.Product.UnitPrice)
            };

            foreach (var item in cartItems)
            {
                order.OrderItems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.Product.UnitPrice
                });
            }

            await _repo.AddAsync(order);

            // حذف عناصر السلة بعد نقلها للأوردر
            await _repo.DeleteCartItems(cartItems);

            await _repo.SaveChangesAsync();

            return order.Id;
        }
    }
}