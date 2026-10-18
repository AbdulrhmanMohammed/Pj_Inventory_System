using Pj_Inventory_System.Application.Dtos.CartItemDtos;

namespace Pj_Inventory_System.API.Services.CartItemService.Base
{
    public interface ICartItemService
    {
        Task<List<CartItemDto>> GetUserCart(int userId);
        Task<CartItemDto> AddToCart(CreateCartItemDto dto);
        Task<bool> UpdateCart(UpdateCartItemDto dto);
        Task<bool> RemoveFromCart(int cartItemId);
    }
}
