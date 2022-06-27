using BTL_KTPM.Application.Catalog.Contacts;
using BTL_KTPM.Application.Catalog.Contacts.Dtos;
using BTL_KTPM.Data.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BTL_KTPM.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly BTL_KTPMDbContext _context;
        private readonly IManageContact _manageContact;

        public ContactController(BTL_KTPMDbContext context, IManageContact manageContact)
        {
            _context = context;
            _manageContact = manageContact;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categories = await _manageContact.GetAll();
            return Ok(categories);
        }
      
        [HttpGet("{ContactId}")]
        public async Task<IActionResult> GetbyId(int ContactId)
        {
            var category = await _manageContact.GetById(ContactId);
            if (category == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(category);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateContactRequest request)
        {
            var result = await _manageContact.Create(request);
            if (result == 0)
                return BadRequest("Lỗi gì đó rồi");
            var category = await _manageContact.GetById(result);
            return Created(nameof(GetbyId), category);
        }


        [HttpDelete("{ContactId}")]
        public async Task<IActionResult> Delete(int ContactId)
        {
            var Result = await _manageContact.Delete(ContactId);
            if (Result == 0)
                return BadRequest();

            return Ok();
        }
    }
}
