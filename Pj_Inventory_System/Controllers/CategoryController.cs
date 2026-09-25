using Microsoft.AspNetCore.Mvc;
using Pj_Inventory_System.Data;
using Pj_Inventory_System.Models;

namespace Pj_Inventory_System.Controllers
{
    public class CategoryController : Controller
    {
        private readonly InventorySystemDbContext _db;

        public CategoryController(InventorySystemDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Category> categories = _db.Categories.ToList();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(category.UID))
                    category.UID = Guid.NewGuid().ToString();

                _db.Categories.Add(category);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // -----------------------------
        // EDIT USING UID
        // -----------------------------
        [HttpGet]
        public IActionResult Edit(string uid)
        {
            var category = _db.Categories.FirstOrDefault(x => x.UID == uid);
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            var existing = _db.Categories.FirstOrDefault(x => x.UID == category.UID);

            if (existing == null)
                return NotFound();

            existing.CategoryName = category.CategoryName;

            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        // -----------------------------
        // DELETE USING UID
        // -----------------------------
        [HttpGet]
        public IActionResult Delete(string uid)
        {
            var category = _db.Categories.FirstOrDefault(x => x.UID == uid);
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(string uid)
        {
            var category = _db.Categories.FirstOrDefault(x => x.UID == uid);

            if (category != null)
            {
                _db.Categories.Remove(category);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
