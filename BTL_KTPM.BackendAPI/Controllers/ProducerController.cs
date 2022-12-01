using BTL_KTPM.Application.Catalog.Producers;
using BTL_KTPM.Application.Catalog.Producers.Dtos;
using BTL_KTPM.Data.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BTL_KTPM.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducerController : ControllerBase
    {
        private readonly BTL_KTPMDbContext _context;
        private readonly IManageProducer _manageProducer;

        public ProducerController(BTL_KTPMDbContext context, IManageProducer manageProducer)
        {
            _context = context;
            _manageProducer = manageProducer;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categories = await _manageProducer.GetAllProducer();
            return Ok(categories);
        }
        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetProducerRequest request)
        {
            var categories = await _manageProducer.GetAlllPaging(request);
            return Ok(categories);
        }

        [HttpGet("{ProducerId}")]
        public async Task<IActionResult> GetbyId(int ProducerId)
        {
            var category = await _manageProducer.GetById(ProducerId);
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
        public async Task<IActionResult> Create(CreateProducerRequest request)
        {
            var result = await _manageProducer.Create(request);
            if (result == 0)
                return BadRequest("Lỗi gì đó rồi");
            var category = await _manageProducer.GetById(result);
            return Created(nameof(GetbyId), category);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateProducerRequest request)
        {
            var Result = await _manageProducer.Update(request);
            if (Result == 0)
                return BadRequest();
            return Ok();
        }

        [HttpDelete("{ProducerId}")]
        public async Task<IActionResult> Delete(int ProducerId)
        {
            var Result = await _manageProducer.Delete(ProducerId);
            if (Result == 0)
                return BadRequest();

            return Ok();
        }
    }
}
