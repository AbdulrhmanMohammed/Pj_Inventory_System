using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pj_Inventory_System.Data;
using Pj_Inventory_System.Models;

namespace Pj_Inventory_System.Controllers
{
    public class StockInController : Controller
    {
        private readonly InventorySystemDbContext _db;

        public StockInController(InventorySystemDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var stockIn = _db.StockIn
                .Include(s => s.Product)
                .ToList();

            return View(stockIn);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Products = new SelectList(_db.Products.ToList(), "ProductID", "ProductName");
            return View();
        }

        [HttpPost]
        public IActionResult Create(StockIn stockIn)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(stockIn.UID))
                    stockIn.UID = Guid.NewGuid().ToString();

                _db.StockIn.Add(stockIn);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Products = new SelectList(_db.Products.ToList(), "ProductID", "ProductName");
            return View(stockIn);
        }

        // -----------------------------
        // EDIT USING UID
        // -----------------------------
        [HttpGet]
        public IActionResult Edit(string uid)
        {
            var stockIn = _db.StockIn
                .Include(s => s.Product)
                .FirstOrDefault(x => x.UID == uid);

            if (stockIn == null) return NotFound();

            ViewBag.Products = new SelectList(_db.Products.ToList(), "ProductID", "ProductName");
            return View(stockIn);
        }

        [HttpPost]
        public IActionResult Edit(StockIn stockIn)
        {
            var existing = _db.StockIn.FirstOrDefault(x => x.UID == stockIn.UID);

            if (existing == null)
                return NotFound();

            existing.ProductID = stockIn.ProductID;
            existing.Quantity = stockIn.Quantity;
            existing.DateIn = stockIn.DateIn;

            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        // -----------------------------
        // DELETE USING UID
        // -----------------------------
        [HttpGet]
        public IActionResult Delete(string uid)
        {
            var stockIn = _db.StockIn
                .Include(s => s.Product)
                .FirstOrDefault(s => s.UID == uid);

            if (stockIn == null) return NotFound();
            return View(stockIn);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(string uid)
        {
            var stockIn = _db.StockIn.FirstOrDefault(x => x.UID == uid);

            if (stockIn != null)
            {
                _db.StockIn.Remove(stockIn);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
