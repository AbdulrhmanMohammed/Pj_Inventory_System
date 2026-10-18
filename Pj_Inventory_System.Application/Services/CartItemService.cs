using Pj_Inventory_System.API.Services.CartItemService.Base;
using Pj_Inventory_System.Application.Dtos.CartItemDtos;
using Pj_Inventory_System.Domain.Models;
using Pj_Inventory_System.Infrastructure.Repositories.CartItemRepository;

namespace Pj_Inventory_System.API.Services.CartItemService
{
    public class CartItemService : ICartItemService
    {
        private readonly ICartItemRepository _repo;

        public CartItemService(ICartItemRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<CartItemDto>> GetUserCart(int userId)
        {
            var items = await _repo.GetByUserAsync(userId);

            return items.Select(x => new CartItemDto
            {
                CartItemID = x.Id,
                UID = x.Id.ToString(),
                UserId = x.UserId,
                ProductId = x.ProductId,
                Quantity = x.Quantity
            }).ToList();
        }

        public async Task<CartItemDto> AddToCart(CreateCartItemDto dto)
        {
            var item = new CartItem
            {
                UserId = dto.UserId,
                ProductId = dto.ProductId,
                Quantity = dto.Quantity
            };

            await _repo.AddAsync(item);
            await _repo.SaveAsync();

            return new CartItemDto
            {
                CartItemID = item.Id,
                UID = item.Id.ToString(),
                UserId = item.UserId,
                ProductId = item.ProductId,
                Quantity = item.Quantity
            };
        }

        public async Task<bool> UpdateCart(UpdateCartItemDto dto)
        {
            var item = await _repo.GetByIdAsync(dto.CartItemID);
            if (item == null)
                return false;

            item.Quantity = dto.Quantity;

            await _repo.UpdateAsync(item);
            await _repo.SaveAsync();

            return true;
        }

        public async Task<bool> RemoveFromCart(int cartItemId)
        {
            await _repo.DeleteAsync(cartItemId);
            await _repo.SaveAsync();
            return true;
        }
    }
}
