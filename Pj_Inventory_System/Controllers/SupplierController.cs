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
                if (string.IsNullOrEmpty(supplier.UID))
                    supplier.UID = Guid.NewGuid().ToString();

                _db.Supplier.Add(supplier);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(supplier);
        }

        // -----------------------------
        // EDIT USING UID
        // -----------------------------
        [HttpGet]
        public IActionResult Edit(string uid)
        {
            var supplier = _db.Supplier.FirstOrDefault(x => x.UID == uid);
            if (supplier == null) return NotFound();
            return View(supplier);
        }

        [HttpPost]
        public IActionResult Edit(Supplier supplier)
        {
            var existing = _db.Supplier.FirstOrDefault(x => x.UID == supplier.UID);

            if (existing == null)
                return NotFound();

            existing.SupplierName = supplier.SupplierName;

            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        // -----------------------------
        // DELETE USING UID
        // -----------------------------
        [HttpGet]
        public IActionResult Delete(string uid)
        {
            var supplier = _db.Supplier.FirstOrDefault(x => x.UID == uid);
            if (supplier == null) return NotFound();
            return View(supplier);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(string uid)
        {
            var supplier = _db.Supplier.FirstOrDefault(x => x.UID == uid);

            if (supplier != null)
            {
                _db.Supplier.Remove(supplier);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
