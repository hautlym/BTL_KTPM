using BTL_KTPM.Application.Catalog.Users;
using BTL_KTPM.Application.Catalog.Users.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BTL_KTPM.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginController : ControllerBase
    {
        private readonly IManagerUser _managerUser;

        public UserLoginController(IManagerUser managerUser)
        {
            _managerUser = managerUser;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cart = await _managerUser.GetAllUser();
            return Ok(cart);
        }

        [HttpGet("{UserId}")]
        public async Task<IActionResult> GetbyId(int UserId)
        {
            var user = await _managerUser.GetById(UserId);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(user);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Create([FromQuery] CreateUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var result = await _managerUser.Create(request);
            if (result == 0)
                return BadRequest("Không thành công");
            if (result == -1)
            {
                return BadRequest("Tài khoản đã tồn tại");
            }
            var user = await _managerUser.GetById(result);
            return Created(nameof(GetbyId), user);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromQuery] UpdateUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Result = await _managerUser.Update(request);
            if (Result == 0)
                return BadRequest();
            return Ok();
        }

        [HttpDelete("{UserId}")]
        public async Task<IActionResult> Delete(int UserId)
        {
            var Result = await _managerUser.Delete(UserId);
            if (Result == 0)
                return BadRequest();

            return Ok();
        }
        [HttpGet("login")]
        public async Task<IActionResult> Login([FromQuery] string userName, string Pass)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _managerUser.Login(userName, Pass);
            if (result == null)
                return BadRequest("No find Id");
            return Ok(result);
        }
    }
}