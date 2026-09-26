using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pj_Inventory_System.Dtos.StockOutDtos;
using Pj_Inventory_System.Models;
using Pj_Inventory_System.Repositories;

namespace Pj_Inventory_System.Controllers
{
    public class StockOutController : Controller
    {
        private readonly IStockOutRepository _stockOutRepo;
        private readonly IProductRepository _productRepo;

        public StockOutController(
            IStockOutRepository stockOutRepo,
            IProductRepository productRepo)
        {
            _stockOutRepo = stockOutRepo;
            _productRepo = productRepo;
        }

        // ============================
        // LOAD DROPDOWNS
        // ============================
        private void LoadDropDowns()
        {
            ViewBag.Products = new SelectList(
                _productRepo.GetAll(), "ProductID", "ProductName");
        }

        // ============================
        // INDEX
        // ============================
        [HttpGet]
        public IActionResult Index()
        {
            var stockOut = _stockOutRepo.GetAll()
                .Select(s => new StockOutDto
                {
                    StockOutID = s.StockOutID,
                    UID = s.UID,
                    ProductID = s.ProductID,
                    Quantity = s.Quantity,
                    DateOut = s.DateOut,
                    ProductName = s.Product?.ProductName
                })
                .ToList();

            return View(stockOut);
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
        public IActionResult Create(CreateStockOutDto dto)
        {
            LoadDropDowns();

            if (!ModelState.IsValid)
                return View(dto);

            var stockOut = new StockOut
            {
                UID = Guid.NewGuid().ToString(),
                ProductID = dto.ProductID,
                Quantity = dto.Quantity,
                DateOut = dto.DateOut
            };

            _stockOutRepo.Add(stockOut);
            _stockOutRepo.Save();

            return RedirectToAction("Index");
        }

        // ============================
        // EDIT GET (بالـ UID)
        // ============================
        [HttpGet]
        public IActionResult Edit(string uid)
        {
            var stockOut = _stockOutRepo.GetByUid(uid);
            if (stockOut == null) return NotFound();

            var dto = new UpdateStockOutDto
            {
                StockOutID = stockOut.StockOutID,
                UID = stockOut.UID,
                ProductID = stockOut.ProductID,
                Quantity = stockOut.Quantity,
                DateOut = stockOut.DateOut
            };

            LoadDropDowns();
            return View(dto);
        }

        // ============================
        // EDIT POST
        // ============================
        [HttpPost]
        public IActionResult Edit(UpdateStockOutDto dto)
        {
            var existing = _stockOutRepo.GetByUid(dto.UID);
            if (existing == null) return NotFound();

            existing.ProductID = dto.ProductID;
            existing.Quantity = dto.Quantity;
            existing.DateOut = dto.DateOut;

            _stockOutRepo.Update(existing);
            _stockOutRepo.Save();

            return RedirectToAction("Index");
        }

        // ============================
        // DELETE GET (بالـ UID)
        // ============================
        [HttpGet]
        public IActionResult Delete(string uid)
        {
            var stockOut = _stockOutRepo.GetByUid(uid);
            if (stockOut == null) return NotFound();

            var dto = new StockOutDto
            {
                StockOutID = stockOut.StockOutID,
                UID = stockOut.UID,
                ProductID = stockOut.ProductID,
                Quantity = stockOut.Quantity,
                DateOut = stockOut.DateOut,
                ProductName = stockOut.Product?.ProductName
            };

            return View(dto);
        }

        // ============================
        // DELETE POST
        // ============================
        [HttpPost]
        public IActionResult DeleteConfirmed(string uid)
        {
            var stockOut = _stockOutRepo.GetByUid(uid);
            if (stockOut == null) return NotFound();

            _stockOutRepo.Delete(stockOut);
            _stockOutRepo.Save();

            return RedirectToAction("Index");
        }
    }
}
