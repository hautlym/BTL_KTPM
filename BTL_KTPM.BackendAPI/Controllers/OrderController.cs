using BTL_KTPM.Application.Catalog.Orders;
using BTL_KTPM.Application.Catalog.Orders.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BTL_KTPM.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IManageOrder _manageOrder;

        public OrderController(IManageOrder manageOrder)
        {
            _manageOrder = manageOrder;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]GetOrderRequest request)
        {
            var cart = await _manageOrder.GetAlllPaging(request);
            return Ok(cart);
        }

        [HttpGet("UserId/{UserId}")]
        public async Task<IActionResult> GetByUserId(Guid UserId)
        {
            var cart = await _manageOrder.GetAllCartByUserId(UserId);
            if (cart.Count == 0)
            {
                return NotFound();
            }
            return Ok(cart);
        }

        [HttpGet("{OrderId}")]
        public async Task<IActionResult> GetbyId(int OrderId)
        {
            var category = await _manageOrder.GetById(OrderId);
            if (category == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(category);
            }
        }

        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] CreateOrderRequest request)
        {
            var result = await _manageOrder.Create(request);
            if (result == 0)
                return BadRequest();
            var category = await _manageOrder.GetById(result);
            return Created(nameof(GetbyId), category);
        }

        [HttpDelete()]
        public async Task<IActionResult> Delete([FromQuery]int CartID)
        {
            var Result = await _manageOrder.Delete(CartID);
            if (Result == 0)
                return BadRequest();

            return Ok();
        }
        [HttpDelete("Cart/{UserId}")]
        public async Task<IActionResult> DeleteCart(Guid UserId)
        {
            var Result = await _manageOrder.DeleteCart(UserId);
            if (Result == 0)
                return BadRequest();

            return Ok();
        }
        [HttpDelete("OrderDetail/{OrderId}")]
        public async Task<IActionResult> DeleteOrderDetail(int OrderId)
        {
            var Result = await _manageOrder.DeleteOrderTail(OrderId);
            if (Result == 0)
                return BadRequest();

            return Ok();
        }
    }
}
