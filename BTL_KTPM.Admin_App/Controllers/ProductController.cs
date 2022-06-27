using BTL_KTPM.ApiIntegration.Service.CategoryApiClient;
using BTL_KTPM.ApiIntegration.Service.ProductApiClient;
using BTL_KTPM.Application.Catalog.Products.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BTL_KTPM.Admin_App.Controllers
{
    public class ProductController : BaseController
    {

        private readonly IProductApiClient _productApiClient;
        private readonly IConfiguration _configuration;
        private readonly ICategoriesApiClient _categoriesApiClient;

        public ProductController(IProductApiClient productApiClient, IConfiguration configuration, ICategoriesApiClient categoriesApiClient)
        {
            _productApiClient = productApiClient;
            _configuration = configuration;
            _categoriesApiClient = categoriesApiClient;
        }
        public async Task<IActionResult> Index(string keyword,int? categoryId, int PageIndex = 1, int PageSize = 5)
        {
            var request = new GetProductPagingRequest()
            {
                PageIndex = PageIndex,
                PageSize = PageSize,
                keyword = keyword,
                CategoryId = categoryId
            };
            var categories =await _categoriesApiClient.GetAll();
            ViewBag.Categories = categories.ResultObj.Select(x => new SelectListItem()
            {
                Text = x.CategoryName,
                Value = x.Id.ToString(),
                Selected = categoryId.HasValue && categoryId.Value == x.Id 
            }); 
            var data = await _productApiClient.GetPaging(request);
            ViewBag.Keyword = keyword;
            if (TempData["result"] != null)
            {
                ViewBag.SuscessMsg = TempData["result"];
            }
            return View(data.ResultObj);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoriesApiClient.GetAll();
            ViewBag.Categories = categories.ResultObj.Select(x => new SelectListItem()
            {
                Text = x.CategoryName,
                Value = x.Id.ToString(),
            });
            return View();
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm]ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
                return View(ModelState);
            
            var result = await _productApiClient.CreateProduct(request);
            if (result)
            {
                TempData["result"] = "Thêm mới sản phẩm thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Thêm sản phẩm thất bại");
            return View(request);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _productApiClient.GetById(id);
            if (result !=null)
            {
                var product = result;
                var updateRequest = new ProductUpdateRequest()
                {
                    ProductName = product.ProductName,
                    ProductDescription = product.ProductDescription,
                    CategoryId = product.CategoryId,
                    ProducerId = product.ProducerId,
                    ProductOriginalPrice = product.ProductOriginalPrice,
                    ProductTitle = product.ProductTitle,
                    Discount = product.Discount,
                    IsNewProduct = product.IsNewProduct,
                    Id = product.Id,
                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Edit(ProductUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _productApiClient.UpdateProduct(request.Id, request);
            if (result)
            {
                TempData["result"] = "Cập nhật thông tin thành công";
                return RedirectToAction("Index");

            }

            ModelState.AddModelError("","Thay đổi không thành công");
            return View(request);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(new ProductDeleteRequest
            {
                Id = id,
            });
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var result = await _productApiClient.GetById(id);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(ProductDeleteRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _productApiClient.Delete(request.Id);
            if (result)
            {
                TempData["result"] = "Xóa tài khoản thành công";
                return RedirectToAction("Index");

            }

            ModelState.AddModelError("", "Xóa không thành công");
            return View(request);
        }
    }
}
