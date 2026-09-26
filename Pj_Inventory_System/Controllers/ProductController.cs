using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pj_Inventory_System.Dtos.ProductDtos;
using Pj_Inventory_System.Models;
using Pj_Inventory_System.Repositories;

namespace Pj_Inventory_System.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepo;
        private readonly ICategoryRepository _categoryRepo;
        private readonly ISupplierRepository _supplierRepo;

        public ProductController(
            IProductRepository productRepo,
            ICategoryRepository categoryRepo,
            ISupplierRepository supplierRepo)
        {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
            _supplierRepo = supplierRepo;
        }

        private void LoadDropDowns()
        {
            ViewBag.Categories = new SelectList(
                _categoryRepo.GetAll(), "CategoryID", "CategoryName");

            ViewBag.Suppliers = new SelectList(
                _supplierRepo.GetAll(), "SupplierID", "SupplierName");
        }

        // ============================
        // INDEX
        // ============================
        [HttpGet]
        public IActionResult Index()
        {
            var products = _productRepo.GetAll()
                .Select(p => new ProductDto
                {
                    ProductID = p.ProductID,
                    UID = p.UID,
                    ProductName = p.ProductName,
                    CategoryID = p.CategoryID,
                    SupplierID = p.SupplierID,
                    QuantityInStock = p.QuantityInStock,
                    UnitPrice = p.UnitPrice,
                    imageUrl = p.imageUrl,

                    CategoryName = p.Category?.CategoryName ?? "",
                    SupplierName = p.Supplier?.SupplierName ?? ""
                }).ToList();

            return View(products);
        }

        // ============================
        // CREATE GET
        // ============================
        [HttpGet]
        public IActionResult Create()
        {
            LoadDropDowns();
            return View();
        }

        // ============================
        // CREATE POST
        // ============================
        [HttpPost]
        public IActionResult Create(CreateProductDto dto, IFormFile image)
        {
            LoadDropDowns();

            if (!ModelState.IsValid)
                return View(dto);

            var product = new Product
            {
                UID = Guid.NewGuid().ToString(),
                ProductName = dto.ProductName,
                CategoryID = dto.CategoryID,
                SupplierID = dto.SupplierID,
                QuantityInStock = dto.QuantityInStock,
                UnitPrice = dto.UnitPrice,
                imageUrl = dto.imageUrl
            };

            if (image != null && image.Length > 0)
                product.imageUrl = UploadImage(image);

            _productRepo.Add(product);
            _productRepo.Save();

            return RedirectToAction("Index");
        }

        // ============================
        // EDIT GET (بالـ UID)
        // ============================
        [HttpGet]
        public IActionResult Edit(string uid)
        {
            var product = _productRepo.GetByUid(uid);
            if (product == null) return NotFound();

            var dto = new UpdateProductDto
            {
                ProductID = product.ProductID,
                UID = product.UID,
                ProductName = product.ProductName,
                CategoryID = product.CategoryID,
                SupplierID = product.SupplierID,
                QuantityInStock = product.QuantityInStock,
                UnitPrice = product.UnitPrice,
                imageUrl = product.imageUrl
            };

            LoadDropDowns();
            return View(dto);
        }

        // ============================
        // EDIT POST
        // ============================
        [HttpPost]
        public IActionResult Edit(UpdateProductDto dto, IFormFile image)
        {
            LoadDropDowns();

            var existing = _productRepo.GetByUid(dto.UID);
            if (existing == null) return NotFound();

            existing.ProductName = dto.ProductName;
            existing.UnitPrice = dto.UnitPrice;
            existing.CategoryID = dto.CategoryID;
            existing.SupplierID = dto.SupplierID;
            existing.QuantityInStock = dto.QuantityInStock;

            if (image != null && image.Length > 0)
                existing.imageUrl = UploadImage(image);

            _productRepo.Update(existing);
            _productRepo.Save();

            return RedirectToAction("Index");
        }

        // ============================
        // DELETE GET (بالـ UID)
        // ============================
        [HttpGet]
        public IActionResult Delete(string uid)
        {
            var product = _productRepo.GetByUid(uid);
            if (product == null) return NotFound();

            var dto = new ProductDto
            {
                ProductID = product.ProductID,
                UID = product.UID,
                ProductName = product.ProductName,
                CategoryID = product.CategoryID,
                SupplierID = product.SupplierID,
                QuantityInStock = product.QuantityInStock,
                UnitPrice = product.UnitPrice,
                imageUrl = product.imageUrl,
                CategoryName = product.Category?.CategoryName,
                SupplierName = product.Supplier?.SupplierName
            };

            return View(dto);
        }

        // ============================
        // DELETE POST
        // ============================
        [HttpPost]
        public IActionResult DeleteConfirmed(string uid)
        {
            var existing = _productRepo.GetByUid(uid);
            if (existing == null) return NotFound();

            _productRepo.Delete(existing);
            _productRepo.Save();

            return RedirectToAction("Index");
        }

        // ============================
        // رفع الصورة
        // ============================
        private string UploadImage(IFormFile image)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);

            string folderPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "images",
                "product"
            );

            Directory.CreateDirectory(folderPath);

            string filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(stream);
            }

            return "/images/product/" + fileName;
        }
    }
}
