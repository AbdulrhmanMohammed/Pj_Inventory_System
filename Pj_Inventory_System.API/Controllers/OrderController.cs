using Microsoft.AspNetCore.Mvc;
using Pj_Inventory_System.Application.Services.Base;

namespace Second_ASP_EF.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(
            IOrderService orderService)
        {
            _orderService = orderService;
        }

        // POST: api/Orders/5
        [HttpPost("{userId}")]
        public async Task<IActionResult> MakeOrder(
            int userId)
        {
            int orderId =
                await _orderService.MakeOrder(userId);

            if (orderId == 0)
                return BadRequest(
                    "Cart is empty");

            return Ok(new
            {
                Message = "Order created successfully",
                OrderId = orderId
            });
        }

        // GET: api/Orders/user/5
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetMyOrders(
            int userId)
        {
            var orders =
                await _orderService.GetMyOrders(userId);

            var result =
                orders.Select(order => new
                {
                    order.Id,
                    order.UserId,
                    order.Status,
                    order.CreatedAt,
                    order.TotalAmount,

                    Items = order.OrderItems.Select(item => new
                    {
                        item.Id,
                        item.ProductId,
                        item.Quantity,
                        item.UnitPrice,

                        ProductName =
                            item.Product.ProductName
                    }).ToList()
                });

            return Ok(result);
        }
    }
}