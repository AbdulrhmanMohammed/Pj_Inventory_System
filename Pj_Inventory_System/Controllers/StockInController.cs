using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pj_Inventory_System.Dtos.StockInDtos;
using Pj_Inventory_System.Models;
using Pj_Inventory_System.Repositories;

namespace Pj_Inventory_System.Controllers
{
    public class StockInController : Controller
    {
        private readonly IStockInRepository _stockInRepo;
        private readonly IProductRepository _productRepo;

        public StockInController(
            IStockInRepository stockInRepo,
            IProductRepository productRepo)
        {
            _stockInRepo = stockInRepo;
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
            var stockIn = _stockInRepo.GetAll()
                .Select(s => new StockInDto
                {
                    StockInID = s.StockInID,
                    UID = s.UID,
                    ProductID = s.ProductID,
                    Quantity = s.Quantity,
                    DateIn = s.DateIn,
                    ProductName = s.Product?.ProductName
                })
                .ToList();

            return View(stockIn);
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
        public IActionResult Create(CreateStockInDto dto)
        {
            LoadDropDowns();

            if (!ModelState.IsValid)
                return View(dto);

            var stockIn = new StockIn
            {
                UID = Guid.NewGuid().ToString(),
                ProductID = dto.ProductID,
                Quantity = dto.Quantity,
                DateIn = dto.DateIn
            };

            _stockInRepo.Add(stockIn);
            _stockInRepo.Save();

            return RedirectToAction("Index");
        }

        // ============================
        // EDIT GET (بالـ UID)
        // ============================
        [HttpGet]
        public IActionResult Edit(string uid)
        {
            var stockIn = _stockInRepo.GetByUid(uid);
            if (stockIn == null) return NotFound();

            var dto = new UpdateStockInDto
            {
                StockInID = stockIn.StockInID,
                UID = stockIn.UID,
                ProductID = stockIn.ProductID,
                Quantity = stockIn.Quantity,
                DateIn = stockIn.DateIn
            };

            LoadDropDowns();
            return View(dto);
        }

        // ============================
        // EDIT POST
        // ============================
        [HttpPost]
        public IActionResult Edit(UpdateStockInDto dto)
        {
            var existing = _stockInRepo.GetByUid(dto.UID);
            if (existing == null) return NotFound();

            existing.ProductID = dto.ProductID;
            existing.Quantity = dto.Quantity;
            existing.DateIn = dto.DateIn;

            _stockInRepo.Update(existing);
            _stockInRepo.Save();

            return RedirectToAction("Index");
        }

        // ============================
        // DELETE GET (بالـ UID)
        // ============================
        [HttpGet]
        public IActionResult Delete(string uid)
        {
            var stockIn = _stockInRepo.GetByUid(uid);
            if (stockIn == null) return NotFound();

            var dto = new StockInDto
            {
                StockInID = stockIn.StockInID,
                UID = stockIn.UID,
                ProductID = stockIn.ProductID,
                Quantity = stockIn.Quantity,
                DateIn = stockIn.DateIn,
                ProductName = stockIn.Product?.ProductName
            };

            return View(dto);
        }

        // ============================
        // DELETE POST
        // ============================
        [HttpPost]
        public IActionResult DeleteConfirmed(string uid)
        {
            var stockIn = _stockInRepo.GetByUid(uid);
            if (stockIn == null) return NotFound();

            _stockInRepo.Delete(stockIn);
            _stockInRepo.Save();

            return RedirectToAction("Index");
        }
    }
}
