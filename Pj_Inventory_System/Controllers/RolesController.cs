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

        // =========================
        // GET: Assign Permissions
        // =========================
        public IActionResult AssignPermissions(int roleId)
        {
            var role = _db.Roles.Find(roleId);

            if (role == null)
            {
                return NotFound();
            }

            var allPermissions = _db.Permissions.ToList();

            var assignedPermissions = _db.PermissionRoles
                .Where(pr => pr.RolesId == roleId)
                .Select(pr => pr.PermissionsId)
                .ToList();

            ViewBag.AllPermissions = allPermissions;
            ViewBag.AssignedPermissions = assignedPermissions;

            return View(role);
        }

        // =========================
        // POST: Save Permissions
        // =========================
        [HttpPost]
        public IActionResult AssignPermissions(int roleId, List<int> permissionIds)
        {
            var role = _db.Roles.Find(roleId);

            if (role == null)
            {
                return NotFound();
            }

            // 1. Remove old permissions
            var oldPermissions = _db.PermissionRoles
                .Where(pr => pr.RolesId == roleId)
                .ToList();

            _db.PermissionRoles.RemoveRange(oldPermissions);

            // 2. Add new permissions
            if (permissionIds != null)
            {
                foreach (var permissionId in permissionIds)
                {
                    var permissionRole = new PermissionRole
                    {
                        RolesId = roleId,
                        PermissionsId = permissionId
                    };

                    _db.PermissionRoles.Add(permissionRole);
                }
            }

            // 3. Save
            _db.SaveChanges();

            return RedirectToAction("AssignPermissions", new { roleId = roleId });
        }
    }
}
