using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pj_Inventory_System.Domain.Models;
using Pj_Inventory_System.Infrastructure.Data;

namespace Second_ASP_EF.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly InventorySystemDbContext _context;

        public CartController(InventorySystemDbContext context)
        {
            _context = context;
        }

        // GET: api/Cart/1
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCart(int userId)
        {
            var items = await _context.CartItems
                .Include(x => x.Product)
                .Where(x => x.UserId == userId)
                .Select(x => new
                {
                    x.Id,
                    x.ProductId,
                    x.Quantity,
                    Name = x.Product.ProductName,
                    Price = x.Product.UnitPrice,
                    ImageUrl = x.Product.imageUrl
                })
                .ToListAsync();

            return Ok(items);
        }

        // POST: api/Cart
        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] AddCartDto dto)
        {
            if (dto.Quantity < 1)
                return BadRequest("Invalid quantity");

            var product = await _context.Products
                .FirstOrDefaultAsync(x =>
                    x.ProductID == dto.ProductId);

            if (product == null)
                return NotFound("Product not found");

            if (product.QuantityInStock <= 0)
                return BadRequest("Out Of Stock");

            var item = await _context.CartItems
                .FirstOrDefaultAsync(x =>
                    x.UserId == dto.UserId &&
                    x.ProductId == dto.ProductId);

            if (item == null)
            {
                item = new CartItem
                {
                    UserId = dto.UserId,
                    ProductId = dto.ProductId,
                    Quantity = dto.Quantity
                };

                _context.CartItems.Add(item);
            }
            else
            {
                if (item.Quantity + 1 >
                    product.QuantityInStock)
                {
                    return BadRequest(
                        "Not enough stock");
                }

                item.Quantity += dto.Quantity;
            }

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Added to cart"
            });
        }

        // PUT: api/Cart/increase/1/5
        [HttpPut("increase/{userId}/{productId}")]
        public async Task<IActionResult> IncreaseQuantity(
            int userId,
            int productId)
        {
            var item = await _context.CartItems
                .Include(x => x.Product)
                .FirstOrDefaultAsync(x =>
                    x.UserId == userId &&
                    x.ProductId == productId);

            if (item == null)
                return NotFound();

            if (item.Quantity >=
                item.Product.QuantityInStock)
            {
                return BadRequest(
                    "Not enough stock");
            }

            item.Quantity++;

            await _context.SaveChangesAsync();

            return Ok();
        }

        // PUT: api/Cart/decrease/1/5
        [HttpPut("decrease/{userId}/{productId}")]
        public async Task<IActionResult> DecreaseQuantity(
            int userId,
            int productId)
        {
            var item = await _context.CartItems
                .FirstOrDefaultAsync(x =>
                    x.UserId == userId &&
                    x.ProductId == productId);

            if (item == null)
                return NotFound();

            if (item.Quantity > 1)
            {
                item.Quantity--;
            }
            else
            {
                _context.CartItems.Remove(item);
            }

            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/Cart/1/5
        [HttpDelete("{userId}/{productId}")]
        public async Task<IActionResult> RemoveFromCart(
            int userId,
            int productId)
        {
            var item = await _context.CartItems
                .FirstOrDefaultAsync(x =>
                    x.UserId == userId &&
                    x.ProductId == productId);

            if (item == null)
                return NotFound();

            _context.CartItems.Remove(item);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Removed from cart"
            });
        }
    }

    public class AddCartDto
    {
        public int UserId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; } = 1;
    }
}