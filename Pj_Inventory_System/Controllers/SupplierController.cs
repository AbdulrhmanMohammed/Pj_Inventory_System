using Microsoft.AspNetCore.Mvc;
using Pj_Inventory_System.Dtos.SupplierDtos;
using Pj_Inventory_System.Models;
using Pj_Inventory_System.Repositories;

namespace Pj_Inventory_System.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ISupplierRepository _supplierRepo;

        public SupplierController(ISupplierRepository supplierRepo)
        {
            _supplierRepo = supplierRepo;
        }

        // ============================
        // INDEX
        // ============================
        [HttpGet]
        public IActionResult Index()
        {
            var suppliers = _supplierRepo.GetAll()
                .Select(s => new SupplierDto
                {
                    SupplierID = s.SupplierID,
                    UID = s.UID,
                    SupplierName = s.SupplierName,
                    ProductsCount = s.Products != null ? s.Products.Count : 0
                })
                .ToList();

            return View(suppliers);
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
        public IActionResult Create(CreateSupplierDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var supplier = new Supplier
            {
                UID = Guid.NewGuid().ToString(),
                SupplierName = dto.SupplierName
            };

            _supplierRepo.Add(supplier);
            _supplierRepo.Save();

            return RedirectToAction("Index");
        }

        // ============================
        // EDIT GET (بالـ UID)
        // ============================
        [HttpGet]
        public IActionResult Edit(string uid)
        {
            var supplier = _supplierRepo.GetByUid(uid);
            if (supplier == null) return NotFound();

            var dto = new UpdateSupplierDto
            {
                SupplierID = supplier.SupplierID,
                UID = supplier.UID,
                SupplierName = supplier.SupplierName
            };

            return View(dto);
        }

        // ============================
        // EDIT POST
        // ============================
        [HttpPost]
        public IActionResult Edit(UpdateSupplierDto dto)
        {
            var existing = _supplierRepo.GetByUid(dto.UID);

            if (existing == null)
                return NotFound();

            existing.SupplierName = dto.SupplierName;

            _supplierRepo.Update(existing);
            _supplierRepo.Save();

            return RedirectToAction("Index");
        }

        // ============================
        // DELETE GET (بالـ UID)
        // ============================
        [HttpGet]
        public IActionResult Delete(string uid)
        {
            var supplier = _supplierRepo.GetByUid(uid);
            if (supplier == null) return NotFound();

            var dto = new SupplierDto
            {
                SupplierID = supplier.SupplierID,
                UID = supplier.UID,
                SupplierName = supplier.SupplierName,
                ProductsCount = supplier.Products != null ? supplier.Products.Count : 0
            };

            return View(dto);
        }

        // ============================
        // DELETE POST
        // ============================
        [HttpPost]
        public IActionResult DeleteConfirmed(string uid)
        {
            var supplier = _supplierRepo.GetByUid(uid);

            if (supplier != null)
            {
                _supplierRepo.Delete(supplier);
                _supplierRepo.Save();
            }

            return RedirectToAction("Index");
        }
    }
}
