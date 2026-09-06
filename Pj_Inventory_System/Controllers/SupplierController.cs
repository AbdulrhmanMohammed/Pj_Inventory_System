using Microsoft.AspNetCore.Mvc;
using Pj_Inventory_System.Data;
using Pj_Inventory_System.Models;

namespace Pj_Inventory_System.Controllers
{
    public class SupplierController : Controller
    {
        private readonly InventorySystemDbContext _db;

        public SupplierController(InventorySystemDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var suppliers = _db.Supplier.ToList();
            return View(suppliers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                _db.Supplier.Add(supplier);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(supplier);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var supplier = _db.Supplier.Find(id);
            if (supplier == null) return NotFound();
            return View(supplier);
        }

        [HttpPost]
        public IActionResult Edit(Supplier supplier)
        {
            _db.Supplier.Update(supplier);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var supplier = _db.Supplier.Find(id);
            if (supplier == null) return NotFound();
            return View(supplier);
        }

        [HttpPost]
        public IActionResult Delete(Supplier supplier)
        {
            _db.Supplier.Remove(supplier);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
