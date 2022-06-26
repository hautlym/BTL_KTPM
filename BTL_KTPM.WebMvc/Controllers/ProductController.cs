using BTL_KTPM.ApiIntegration.Service.ProductApiClient;
using BTL_KTPM.WebMvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace BTL_KTPM.WebMvc.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductApiClient _productApiClient;
        public ProductController(IProductApiClient productApiClient)
        {
            _productApiClient = productApiClient;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Details(int id)
        {
            var Product =await _productApiClient.GetById(id);
            var allProduct = await _productApiClient.GetAll();
            var suggestProduct = allProduct.Where(x => x.CategoryId == Product.CategoryId).Skip(0).Take(4).ToList();
            var ProductViewModel = new DetailsProductViewModels()
            {
                ProductDeltails = Product,
                SuggestProduct = suggestProduct
            };
            return View(ProductViewModel);
        }
    }
}
