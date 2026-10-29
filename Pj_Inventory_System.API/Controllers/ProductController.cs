using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pj_Inventory_System.Infrastructure.Data;
using Pj_Inventory_System.Domain.Models;

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
                    p.ProductID,
                    p.ProductName,
                    p.QuantityInStock,
                    p.UnitPrice,
                    imageUrl = $"{Request.Scheme}://{Request.Host}/images/product/{p.imageUrl}"
                })
                .ToListAsync();

            return Ok(products);
        }

        // ================= الإضافات الخاصة بعمليات المخزون =================

        public class StockTransactionDto
        {
            public int Quantity { get; set; }
        }

        // 1. Stock In (توريد / إضافة للمخزون)
        [HttpPost("{id}/stock-in")]
        public async Task<IActionResult> StockIn(int id, [FromBody] StockTransactionDto model)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound(new { message = "المنتج غير موجود" });
            }

            if (model.Quantity <= 0)
            {
                return BadRequest(new { message = "الكمية يجب أن تكون أكبر من الصفر" });
            }

            // تسجيل الحركة في جدول StockIn
            var stockIn = new StockIn
            {
                ProductID = id,
                Quantity = model.Quantity,
                DateIn = DateTime.UtcNow
            };
            _db.StockIn.Add(stockIn);

            // تحديث الكمية الإجمالية للمنتج
            product.QuantityInStock += model.Quantity;

            await _db.SaveChangesAsync();

            return Ok(new { message = "تم زيادة المخزون بنجاح", currentStock = product.QuantityInStock });
        }

        // 2. Stock Out (صرف / خصم من المخزون)
        [HttpPost("{id}/stock-out")]
        public async Task<IActionResult> StockOut(int id, [FromBody] StockTransactionDto model)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound(new { message = "المنتج غير موجود" });
            }

            if (model.Quantity <= 0)
            {
                return BadRequest(new { message = "الكمية يجب أن تكون أكبر من الصفر" });
            }

            // التحقق من توفر الكمية قبل الخصم
            if (product.QuantityInStock < model.Quantity)
            {
                return BadRequest(new { message = "الكمية المطلوبة أكبر من المتبقي في المخزون (خلصت الكمية)" });
            }

            // تسجيل الحركة في جدول StockOut
            var stockOut = new StockOut
            {
                ProductID = id,
                Quantity = model.Quantity,
                DateOut = DateTime.UtcNow
            };
            _db.StockOut.Add(stockOut);

            // خصم الكمية من الإجمالي للمنتج
            product.QuantityInStock -= model.Quantity;

            await _db.SaveChangesAsync();

            return Ok(new { message = "تم خصم الكمية بنجاح", currentStock = product.QuantityInStock });
        }
    }
}