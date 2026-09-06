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
                _db.StockOut.Add(stockOut);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Products = new SelectList(_db.Products.ToList(), "ProductID", "ProductName");
            return View(stockOut);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var stockOut = _db.StockOut.Find(id);
            if (stockOut == null) return NotFound();

            ViewBag.Products = new SelectList(_db.Products.ToList(), "ProductID", "ProductName");
            return View(stockOut);
        }

        [HttpPost]
        public IActionResult Edit(StockOut stockOut)
        {
            _db.StockOut.Update(stockOut);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var stockOut = _db.StockOut
                .Include(s => s.Product)
                .FirstOrDefault(s => s.StockOutID == id);

            if (stockOut == null) return NotFound();
            return View(stockOut);
        }

        [HttpPost]
        public IActionResult Delete(StockOut stockOut)
        {
            _db.StockOut.Remove(stockOut);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
