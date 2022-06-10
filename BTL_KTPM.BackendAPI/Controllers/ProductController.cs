using BTL_KTPM.Application.Catalog.Products;
using BTL_KTPM.Application.Catalog.Products.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BTL_KTPM.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IPublicProductService _publicProductService;
        private readonly IManageProductService _manageProductService;
        public ProductController(IPublicProductService publicProductService,IManageProductService manageProductService)
        {
            _publicProductService = publicProductService;
            _manageProductService = manageProductService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var product = await _publicProductService.GetAll();
            return Ok(product);
        }

        [HttpGet("public-paging")]
        public async Task<IActionResult> Get([FromQuery]GetPublicProductRequest request)
        {
            var product =await _publicProductService.getAllByCategoryId(request);
            return Ok(product);
        }
        [HttpGet("{productId}")]
        public async Task<IActionResult> GetById(int productId)
        {
            var product =await _manageProductService.GetById(productId);
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(product);

            }
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromQuery]ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _manageProductService.Create(request);
            if (result == 0)
                return BadRequest();
            var product = await _manageProductService.GetById(result);
           return Created(nameof(GetById), product);
            //return CreatedAtAction(nameof(GetbyId), new { id = result }, product);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromQuery] ProductUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Result = await _manageProductService.Update(request);
            if (Result == 0)
                return BadRequest();

            return Ok();
        }
        [HttpDelete("{productId}")]
        public async Task<IActionResult> Delete( int productId)
        {
            var Result = await _manageProductService.Delete(productId);
            if (Result == 0)
                return BadRequest();

            return Ok();
        }
    }
}
