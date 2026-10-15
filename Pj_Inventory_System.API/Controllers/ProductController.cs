using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pj_Inventory_System.Infrastructure.Data;

namespace Pj_Inventory_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly InventorySystemDbContext _db;

        public ProductController(InventorySystemDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _db.Products
                .Select(p => new
                {
                    p.ProductName,
                    p.QuantityInStock,
                    p.UnitPrice,
                    imageUrl = $"{Request.Scheme}://{Request.Host}/images/product/{p.imageUrl}"
                })
                .ToListAsync();

            return Ok(products);
        }

    }
}
