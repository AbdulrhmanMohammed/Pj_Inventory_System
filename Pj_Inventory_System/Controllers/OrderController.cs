using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Pj_Inventory_System.Application.Dtos.OrderDtos;
using Pj_Inventory_System.Application.Services.Base;

namespace Pj_Inventory_System.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService _service;

        public OrdersController(IOrderService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var order = await _service.GetByIdAsync(id);

            if (order == null)
                return NotFound();

            return View(order);
        }

        public IActionResult Create()
        {
            ViewBag.Statuses = new SelectList(new[]
            {
                "Pending",
                "Processing",
                "Shipped",
                "Delivered",
                "Cancelled"
            });

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Statuses = new SelectList(new[]
                {
                    "Pending",
                    "Processing",
                    "Shipped",
                    "Delivered",
                    "Cancelled"
                });

                return View(dto);
            }

            await _service.CreateAsync(dto);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var order = await _service.GetByIdAsync(id);

            if (order == null)
                return NotFound();

            ViewBag.Statuses = new SelectList(new[]
            {
                "Pending",
                "Processing",
                "Shipped",
                "Delivered",
                "Cancelled"
            }, order.Status);

            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateOrderDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Statuses = new SelectList(new[]
                {
                    "Pending",
                    "Processing",
                    "Shipped",
                    "Delivered",
                    "Cancelled"
                }, dto.Status);

                return View(dto);
            }

            await _service.UpdateAsync(dto);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var order = await _service.GetByIdAsync(id);

            if (order == null)
                return NotFound();

            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(OrderDto dto)
        {
            await _service.DeleteAsync(dto.OrderID);

            return RedirectToAction(nameof(Index));
        }
    }
}