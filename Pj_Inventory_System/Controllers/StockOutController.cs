using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pj_Inventory_System.Data;
using Pj_Inventory_System.Models;

namespace Pj_Inventory_System.Controllers
{
    public class StockOutController : Controller
    {
        private readonly InventorySystemDbContext _db;

        public StockOutController(InventorySystemDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var stockOut = _db.StockOut
                .Include(s => s.Product)
                .ToList();

            return View(stockOut);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Products = new SelectList(_db.Products.ToList(), "ProductID", "ProductName");
            return View();
        }

        [HttpPost]
        public IActionResult Create(StockOut stockOut)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(stockOut.UID))
                    stockOut.UID = Guid.NewGuid().ToString();

                _db.StockOut.Add(stockOut);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Products = new SelectList(_db.Products.ToList(), "ProductID", "ProductName");
            return View(stockOut);
        }

        // -----------------------------
        // EDIT USING UID
        // -----------------------------
        [HttpGet]
        public IActionResult Edit(string uid)
        {
            var stockOut = _db.StockOut
                .Include(s => s.Product)
                .FirstOrDefault(x => x.UID == uid);

            if (stockOut == null) return NotFound();

            ViewBag.Products = new SelectList(_db.Products.ToList(), "ProductID", "ProductName");
            return View(stockOut);
        }

        [HttpPost]
        public IActionResult Edit(StockOut stockOut)
        {
            var existing = _db.StockOut.FirstOrDefault(x => x.UID == stockOut.UID);

            if (existing == null)
                return NotFound();

            existing.ProductID = stockOut.ProductID;
            existing.Quantity = stockOut.Quantity;
            existing.DateOut = stockOut.DateOut;

            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        // -----------------------------
        // DELETE USING UID
        // -----------------------------
        [HttpGet]
        public IActionResult Delete(string uid)
        {
            var stockOut = _db.StockOut
                .Include(s => s.Product)
                .FirstOrDefault(s => s.UID == uid);

            if (stockOut == null) return NotFound();
            return View(stockOut);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(string uid)
        {
            var stockOut = _db.StockOut.FirstOrDefault(x => x.UID == uid);

            if (stockOut != null)
            {
                _db.StockOut.Remove(stockOut);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
