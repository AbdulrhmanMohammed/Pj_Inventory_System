using Microsoft.AspNetCore.Mvc;
using Pj_Inventory_System.Dtos.CategoryDtos;
using Pj_Inventory_System.Models;
using Pj_Inventory_System.Repositories;

namespace Pj_Inventory_System.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _repo;

        public CategoryController(ICategoryRepository repo)
        {
            _repo = repo;
        }

        // ============================
        // INDEX
        // ============================
        [HttpGet]
        public IActionResult Index()
        {
            var categories = _repo.GetAll()
                .Select(c => new CategoryDto
                {
                    CategoryID = c.CategoryID,
                    CategoryName = c.CategoryName,
                    UID = c.UID
                }).ToList();

            return View(categories);
        }

        // ============================
        // CREATE
        // ============================
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateCategoryDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var category = new Category
            {
                CategoryName = dto.CategoryName,
                UID = Guid.NewGuid().ToString()
            };

            _repo.Add(category);
            _repo.Save();

            return RedirectToAction("Index");
        }

        // ============================
        // EDIT USING UID
        // ============================
        [HttpGet]
        public IActionResult Edit(string uid)
        {
            var category = _repo.GetByUid(uid);
            if (category == null) return NotFound();

            var dto = new UpdateCategoryDto
            {
                CategoryID = category.CategoryID,
                CategoryName = category.CategoryName
            };

            return View(dto);
        }

        [HttpPost]
        public IActionResult Edit(UpdateCategoryDto dto)
        {
            var existing = _repo.GetById(dto.CategoryID);

            if (existing == null)
                return NotFound();

            existing.CategoryName = dto.CategoryName;

            _repo.Update(existing);
            _repo.Save();

            return RedirectToAction("Index");
        }

        // ============================
        // DELETE USING UID
        // ============================
        [HttpGet]
        public IActionResult Delete(string uid)
        {
            var category = _repo.GetByUid(uid);
            if (category == null) return NotFound();

            var dto = new CategoryDto
            {
                CategoryID = category.CategoryID,
                CategoryName = category.CategoryName,
                UID = category.UID
            };

            return View(dto);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(string uid)
        {
            var category = _repo.GetByUid(uid);

            if (category != null)
            {
                _repo.Delete(category);
                _repo.Save();
            }

            return RedirectToAction("Index");
        }
    }
}
