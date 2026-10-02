using Microsoft.AspNetCore.Mvc;
using Pj_Inventory_System.Application.Dtos.CategoryDtos;
using Pj_Inventory_System.Application.Services.Base;
using Pj_Inventory_System.Domain.Models;


namespace Pj_Inventory_System.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        // ============================
        // INDEX
        // ============================
        [HttpGet]
        public IActionResult Index()
        {
            var categories = _service.GetAll()
                .Select(c => new CategoryDto
                {
                    CategoryID = c.CategoryID,
                    CategoryName = c.CategoryName,
                    UID = c.UID
                }).ToList();

            return View(categories);
        }

        // ============================
        // CREATE GET
        // ============================
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // ============================
        // CREATE POST
        // ============================
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

            _service.Create(category);

            return RedirectToAction("Index");
        }

        // ============================
        // EDIT GET 
        // ============================
        [HttpGet]
        public IActionResult Edit(string uid)
        {
            if (string.IsNullOrEmpty(uid))
                return NotFound();

            var category = _service.GetByUid(uid);
            if (category == null) return NotFound();

            var dto = new UpdateCategoryDto
            {
                CategoryID = category.CategoryID,
                CategoryName = category.CategoryName,
                UID = category.UID
            };

            return View(dto);
        }

        // ============================
        // EDIT POST
        // ============================
        [HttpPost]
        public IActionResult Edit(UpdateCategoryDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var existing = _service.GetById(dto.CategoryID);
            if (existing == null) return NotFound();

            existing.CategoryName = dto.CategoryName;

            _service.Update(existing);

            return RedirectToAction("Index");
        }

        // ============================
        // DELETE GET 
        // ============================
        [HttpGet]
        public IActionResult Delete(string uid)
        {
            if (string.IsNullOrEmpty(uid))
                return NotFound();

            var category = _service.GetByUid(uid);
            if (category == null) return NotFound();

            var dto = new CategoryDto
            {
                CategoryID = category.CategoryID,
                CategoryName = category.CategoryName,
                UID = category.UID
            };

            return View(dto);
        }

        // ============================
        // DELETE POST 
        // ============================
        [HttpPost]
        public IActionResult DeleteConfirmed(string uid)
        {
            var category = _service.GetByUid(uid);
            if (category == null) return NotFound();

            _service.Delete(category);

            return RedirectToAction("Index");
        }
    }
}
