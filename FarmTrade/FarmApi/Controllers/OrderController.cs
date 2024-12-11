using FarmBusiness.Services;
using FarmTradeEntity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FarmApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private OrderService _orderService;
        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost("PlaceOrder")]
        public IActionResult PlaceOrder(Guid userId, int adddress)
        {
            _orderService.PlaceOrder(userId, adddress);
            return Ok("addedd and mailed success");
        }
    }
}
