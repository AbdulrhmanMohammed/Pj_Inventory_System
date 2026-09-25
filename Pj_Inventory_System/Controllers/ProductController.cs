using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pj_Inventory_System.Data;
using Pj_Inventory_System.Models;

namespace Pj_Inventory_System.Controllers
{
    public class ProductController : Controller
    {
        private readonly InventorySystemDbContext _db;

        public ProductController(InventorySystemDbContext db)
        {
            _db = db;
        }

        private void LoadDropDowns()
        {
            ViewBag.Categories = new SelectList(_db.Categories.ToList(), "CategoryID", "CategoryName");
            ViewBag.Suppliers = new SelectList(_db.Supplier.ToList(), "SupplierID", "SupplierName");
        }

        // ============================
        // INDEX
        // ============================
        [HttpGet]
        public IActionResult Index()
        {
            var products = _db.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .ToList();

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
        public IActionResult Create(Product product, IFormFile image)
        {
            LoadDropDowns();

            if (string.IsNullOrEmpty(product.UID))
                product.UID = Guid.NewGuid().ToString();

            if (string.IsNullOrWhiteSpace(product.ProductName))
                ModelState.AddModelError("ProductName", "Product Name is required.");

            if (product.CategoryID <= 0)
                ModelState.AddModelError("CategoryID", "Category is required.");

            if (product.SupplierID <= 0)
                ModelState.AddModelError("SupplierID", "Supplier is required.");

            if (product.QuantityInStock <= 0)
                ModelState.AddModelError("QuantityInStock", "Quantity must be greater than 0.");

            if (product.UnitPrice <= 0)
                ModelState.AddModelError("UnitPrice", "Unit Price must be greater than 0.");

            if (!ModelState.IsValid)
                return View(product);

            if (image != null && image.Length > 0)
                product.imageUrl = UploadImage(image);

            _db.Products.Add(product);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        // ============================
        // EDIT GET (بالـ UID)
        // ============================
        [HttpGet]
        public IActionResult Edit(string uid)
        {
            var product = _db.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .FirstOrDefault(p => p.UID == uid);

            if (product == null) return NotFound();

            LoadDropDowns();
            return View(product);
        }

        // ============================
        // EDIT POST
        // ============================
        [HttpPost]
        public IActionResult Edit(Product product, IFormFile image)
        {
            LoadDropDowns();

            var existing = _db.Products.FirstOrDefault(x => x.UID == product.UID);
            if (existing == null) return NotFound();

            existing.ProductName = product.ProductName;
            existing.UnitPrice = product.UnitPrice;
            existing.CategoryID = product.CategoryID;
            existing.SupplierID = product.SupplierID;
            existing.QuantityInStock = product.QuantityInStock;

            if (image != null && image.Length > 0)
                existing.imageUrl = UploadImage(image);

            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        // ============================
        // DELETE GET (بالـ UID)
        // ============================
        [HttpGet]
        public IActionResult Delete(string uid)
        {
            var product = _db.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .FirstOrDefault(p => p.UID == uid);

            if (product == null) return NotFound();

            return View(product);
        }

        // ============================
        // DELETE POST
        // ============================
        [HttpPost]
        public IActionResult Delete(Product product)
        {
            var existing = _db.Products.FirstOrDefault(x => x.UID == product.UID);
            if (existing == null) return NotFound();

            _db.Products.Remove(existing);
            _db.SaveChanges();

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
