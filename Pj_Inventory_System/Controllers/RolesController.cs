using Microsoft.AspNetCore.Mvc;
using Pj_Inventory_System.Data;
using Pj_Inventory_System.Models;

namespace Pj_Inventory_System.Controllers
{
    public class RolesController : Controller
    {
        private readonly InventorySystemDbContext _db;

        public RolesController(InventorySystemDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Role> roles = _db.Roles.ToList();
            return View(roles);
        }

        // =========================
        // Create
        // =========================
        [HttpPost]
        public IActionResult Create(Role role)
        {
            if (ModelState.IsValid)
            {
                _db.Roles.Add(role);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        // =========================
        // Edit
        // =========================
        [HttpPost]
        public IActionResult Edit(Role role)
        {
            if (ModelState.IsValid)
            {
                _db.Roles.Update(role);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        // =========================
        // Delete
        // =========================
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var role = _db.Roles.Find(id);

            if (role != null)
            {
                _db.Roles.Remove(role);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
