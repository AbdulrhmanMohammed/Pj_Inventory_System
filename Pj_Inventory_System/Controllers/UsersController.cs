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
                if (string.IsNullOrEmpty(user.UID))
                    user.UID = Guid.NewGuid().ToString();

                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);

                _db.Users.Add(user);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // =========================
        // Edit USING UID
        // =========================
        [HttpGet]
        public IActionResult Edit(string uid)
        {
            var user = _db.Users.FirstOrDefault(x => x.UID == uid);

            if (user == null)
                return NotFound();

            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(User user)
        {
            var oldUser = _db.Users.FirstOrDefault(x => x.UID == user.UID);

            if (oldUser == null)
                return NotFound();

            oldUser.Name = user.Name;
            oldUser.Email = user.Email;
            oldUser.Username = user.Username;

            if (!string.IsNullOrEmpty(user.Password))
            {
                oldUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
            }

            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        // =========================
        // Delete USING UID
        // =========================
        [HttpPost]
        public IActionResult Delete(string uid)
        {
            var user = _db.Users.FirstOrDefault(x => x.UID == uid);

            if (user != null)
            {
                _db.Users.Remove(user);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // =========================
        // Manage Roles USING UID
        // =========================
        [HttpGet]
        public IActionResult ManageRoles(string uid)
        {
            var user = _db.Users.FirstOrDefault(x => x.UID == uid);

            if (user == null)
                return NotFound();

            var roles = _db.Roles.ToList();

            var userRoleIds = _db.RoleUsers
                .Where(x => x.UserId == user.Id)
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
            var user = _db.Users.FirstOrDefault(x => x.Id == model.UserId);

            if (user == null)
                return NotFound();

            var oldRoles = _db.RoleUsers
                .Where(x => x.UserId == model.UserId)
                .ToList();

            _db.RoleUsers.RemoveRange(oldRoles);

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

        // =========================
        // Manage Files USING UID
        // =========================

        private string UploadFiles(IFormFile file, string name)
        {
            string fileName = name + "_" + Guid.NewGuid().ToString()
                              + Path.GetExtension(file.FileName);

            string folderPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "Files",
                "Users"
            );

            Directory.CreateDirectory(folderPath);

            string filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return "/Files/Users/" + fileName;
        }

        public IActionResult ManageFiles(string uid)
        {
            var user = _db.Users.FirstOrDefault(e => e.UID == uid);

            if (user == null)
                return NotFound();

            var files = _db.UserFiles.Where(e => e.UserID == user.Id).ToList();
            ViewBag.UserName = user.Username;

            ViewBag.Files = files;

            UserFile userFile = new UserFile();
            userFile.UserID = user.Id;

            return View(userFile);
        }

        [HttpPost]
        public IActionResult ManageFiles(UserFile userFile, IFormFile fileUser)
        {
            if (userFile != null && fileUser != null)
            {
                userFile.FileURL = UploadFiles(fileUser, userFile.Name);
            }

            _db.UserFiles.Add(userFile);
            _db.SaveChanges();

            // نجيب الـ UID الصحيح
            var user = _db.Users.FirstOrDefault(x => x.Id == userFile.UserID);

            if (user == null)
                return NotFound();

            return RedirectToAction(nameof(ManageFiles), new { uid = user.UID });
        }
    }
}
