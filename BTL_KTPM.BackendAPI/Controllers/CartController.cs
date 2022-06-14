using BTL_KTPM.Application.Catalog.Carts;
using BTL_KTPM.Application.Catalog.Carts.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BTL_KTPM.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IManageCart _manageCart;

        public CartController(IManageCart manageCart)
        {
            _manageCart = manageCart;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cart = await _manageCart.GetAllCart();
            return Ok(cart);
        }

        [HttpGet("UserId")]
        public async Task<IActionResult> GetByUserId(int UserId)
        {
            var cart = await _manageCart.GetAllCartByUserId(UserId);
            if(cart.Count == 0)
            {
                return NotFound();
            }
            return Ok(cart);
        }

        [HttpGet("{CartId}")]
        public async Task<IActionResult> GetbyId(int CartId)
        {
            var category = await _manageCart.GetById(CartId);
            if (category == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(category);
            }
        }

        [HttpPost("add_cart")]
        public async Task<IActionResult> Create([FromQuery] CreateCartRequest request)
        {
            var result = await _manageCart.Create(request);
            if (result == 0)
                return BadRequest();
            var category = await _manageCart.GetById(result);
            return Created(nameof(GetbyId), category);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCartRequest request)
        {
            var Result = await _manageCart.Update(request);
            if (Result == 0)
                return BadRequest();
            return Ok();
        }

        [HttpDelete("deleteCart/{CartID}")]
        public async Task<IActionResult> Delete(int CartID)
        {
            var Result = await _manageCart.Delete(CartID);
            if (Result == 0)
                return BadRequest();

            return Ok();
        }
    }
}