using Business.Abstract;
using Entities.Dto_s;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("createOrder")]
        public IActionResult CreateOrder(CreateOrderRequestDto createOrder)
        {
            var result = _orderService.CreateOrder(createOrder);
            if (result.ErrorCode == (int)StatusCodes.Status200OK)
                return Ok(result);
            else
                return BadRequest(result);
        }
    }
}