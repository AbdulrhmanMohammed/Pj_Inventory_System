using Microsoft.AspNetCore.Mvc;
using Pj_Inventory_System.Data;
using Pj_Inventory_System.Dtos;
using Pj_Inventory_System.Models;

namespace Pj_Inventory_System.Controllers
{
    public class UsersController : Controller
    {
        private readonly InventorySystemDbContext _db;

        public UsersController(InventorySystemDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<User> users = _db.Users.ToList();
            return View(users);
        }

        // =========================
        // Create
        // =========================
        [HttpPost]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
                _db.Users.Add(user);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // =========================
        // Edit
        // =========================
        [HttpPost]
        public IActionResult Edit(User user)
        {
            var oldUser = _db.Users.Find(user.Id);

            if (oldUser == null)
            {
                return NotFound();
            }

            oldUser.Name = user.Name;
            oldUser.Email = user.Email;
            oldUser.Username = user.Username;

            // Change password only if user entered a new password
            if (!string.IsNullOrEmpty(user.Password))
            {
                oldUser.PasswordHash =
                    BCrypt.Net.BCrypt.HashPassword(user.Password);
            }

            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        // =========================
        // Delete
        // =========================
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var user = _db.Users.Find(id);
            if (user != null)
            {
                _db.Users.Remove(user);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }



        [HttpGet]
        public IActionResult ManageRoles(int id)
        {
            var user = _db.Users.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            // جميع الصلاحيات
            var roles = _db.Roles.ToList();

            // الصلاحيات الموجودة بالفعل للمستخدم
            var userRoleIds = _db.RoleUsers
                .Where(x => x.UserId == id)
                .Select(x => x.RoleId)
                .ToList();

            var model = new UserRolesVM
            {
                UserId = user.Id,
                UserName = user.Name,

                Roles = roles.Select(role => new RoleCheckVM
                {
                    RoleId = role.Id,
                    RoleName = role.Name,

                    IsSelected = userRoleIds.Contains(role.Id)

                }).ToList()
            };

            return View(model);
        }


        [HttpPost]
        public IActionResult ManageRoles(UserRolesVM model)
        {
            var user = _db.Users.Find(model.UserId);

            if (user == null)
            {
                return NotFound();
            }

            // Get old roles
            var oldRoles = _db.RoleUsers
                .Where(x => x.UserId == model.UserId)
                .ToList();

            // Remove old roles
            _db.RoleUsers.RemoveRange(oldRoles);

            // Add selected roles
            foreach (var role in model.Roles)
            {
                if (role.IsSelected)
                {
                    RoleUser roleUser = new RoleUser
                    {
                        UserId = model.UserId,
                        RoleId = role.RoleId
                    };

                    _db.RoleUsers.Add(roleUser);
                }
            }

            _db.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
