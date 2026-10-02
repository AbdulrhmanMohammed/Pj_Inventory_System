using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pj_Inventory_System.Dtos.StockInDtos;
using Pj_Inventory_System.Models;
using Pj_Inventory_System.Services.Base;

namespace Pj_Inventory_System.Controllers
{
    public class StockInController : Controller
    {
        private readonly IStockInService _stockInService;
        private readonly IProductService _productService;

        public StockInController(
            IStockInService stockInService,
            IProductService productService)
        {
            _stockInService = stockInService;
            _productService = productService;
        }

        // ============================
        // LOAD DROPDOWNS
        // ============================
        private void LoadDropDowns()
        {
            ViewBag.Products = new SelectList(
                _productService.GetAll(), "ProductID", "ProductName");
        }

        // ============================
        // INDEX
        // ============================
        [HttpGet]
        public IActionResult Index()
        {
            var stockIn = _stockInService.GetAll()
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

            _stockInService.Create(stockIn);

            return RedirectToAction("Index");
        }

        // ============================
        // EDIT GET (بالـ UID)
        // ============================
        [HttpGet]
        public IActionResult Edit(string uid)
        {
            var stockIn = _stockInService.GetByUid(uid);
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
            var existing = _stockInService.GetByUid(dto.UID);
            if (existing == null) return NotFound();

            existing.ProductID = dto.ProductID;
            existing.Quantity = dto.Quantity;
            existing.DateIn = dto.DateIn;

            _stockInService.Update(existing);

            return RedirectToAction("Index");
        }

        // ============================
        // DELETE GET (بالـ UID)
        // ============================
        [HttpGet]
        public IActionResult Delete(string uid)
        {
            var stockIn = _stockInService.GetByUid(uid);
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
            var stockIn = _stockInService.GetByUid(uid);
            if (stockIn == null) return NotFound();

            _stockInService.Delete(stockIn);

            return RedirectToAction("Index");
        }
    }
}
