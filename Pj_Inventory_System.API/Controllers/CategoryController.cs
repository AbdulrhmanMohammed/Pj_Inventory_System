using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pj_Inventory_System.Infrastructure.Data;

namespace Pj_Inventory_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly InventorySystemDbContext _db;

        public CategoryController(InventorySystemDbContext db)
        {
            _db = db;
        }

        // GET: api/Category
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _db.Categories
                .Select(c => new
                {
                    c.CategoryID,
                    c.CategoryName   // 🔥 هذا اللي تبغاه
                })
                .ToListAsync();

            return Ok(categories);
        }
    }
}
