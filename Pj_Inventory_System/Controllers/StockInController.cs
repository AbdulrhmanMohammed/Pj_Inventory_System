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
                _db.StockIn.Add(stockIn);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Products = new SelectList(_db.Products.ToList(), "ProductID", "ProductName");
            return View(stockIn);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var stockIn = _db.StockIn.Find(id);
            if (stockIn == null) return NotFound();

            ViewBag.Products = new SelectList(_db.Products.ToList(), "ProductID", "ProductName");
            return View(stockIn);
        }

        [HttpPost]
        public IActionResult Edit(StockIn stockIn)
        {
            _db.StockIn.Update(stockIn);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var stockIn = _db.StockIn
                .Include(s => s.Product)
                .FirstOrDefault(s => s.StockInID == id);

            if (stockIn == null) return NotFound();
            return View(stockIn);
        }

        [HttpPost]
        public IActionResult Delete(StockIn stockIn)
        {
            _db.StockIn.Remove(stockIn);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
