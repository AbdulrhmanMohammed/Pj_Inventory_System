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

        [HttpGet]
        public IActionResult Index()
        {
            var products = _db.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .ToList();

            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_db.Categories.ToList(), "CategoryID", "CategoryName");
            ViewBag.Suppliers = new SelectList(_db.Supplier.ToList(), "SupplierID", "SupplierName");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _db.Products.Add(product);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Categories = new SelectList(_db.Categories.ToList(), "CategoryID", "CategoryName");
            ViewBag.Suppliers = new SelectList(_db.Supplier.ToList(), "SupplierID", "SupplierName");
            return View(product);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _db.Products.Find(id);
            if (product == null) return NotFound();

            ViewBag.Categories = new SelectList(_db.Categories.ToList(), "CategoryID", "CategoryName");
            ViewBag.Suppliers = new SelectList(_db.Supplier.ToList(), "SupplierID", "SupplierName");

            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            _db.Products.Update(product);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = _db.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .FirstOrDefault(p => p.ProductID == id);

            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost]
        public IActionResult Delete(Product product)
        {
            _db.Products.Remove(product);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        private string UploadImage(IFormFile image)
        {
            string fileName = Guid.NewGuid().ToString()
                              + Path.GetExtension(image.FileName);


            string folderPath = Path.Combine(
      Directory.GetCurrentDirectory(),
      "wwwroot",
      "images",
      "product"
  );

            Directory.CreateDirectory(folderPath);


            string filePath = Path.Combine(
          folderPath,
          fileName);



            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(stream);
            }

            return "/images/product/" + fileName;
        }

    }
}
