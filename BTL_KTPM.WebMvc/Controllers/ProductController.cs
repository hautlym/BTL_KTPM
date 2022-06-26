using BTL_KTPM.ApiIntegration.Service.CategoryApiClient;
using BTL_KTPM.ApiIntegration.Service.ProductApiClient;
using BTL_KTPM.Application.Catalog.Producers.Dtos;
using BTL_KTPM.Application.Catalog.Products.Dtos;
using BTL_KTPM.WebMvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace BTL_KTPM.WebMvc.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductApiClient _productApiClient;
        private readonly ICategoriesApiClient _categoryApiClient;
        public ProductController(IProductApiClient productApiClient, ICategoriesApiClient categoryApiClient)
        {
            _productApiClient = productApiClient;
            _categoryApiClient = categoryApiClient;
        }
        public async Task<IActionResult> Index(int categoryId = 0, int SortOder = 4)
        {
            var productRequest = new GetProductPagingRequest()
            {
                PageIndex = 1,
                PageSize = 6,
                keyword = "",
                CategoryId = categoryId
            };
            var product = await _productApiClient.GetPaging(productRequest);
            var listProduct = product.ResultObj.Items;
            if (SortOder != 0)
            {
                switch (SortOder)
                {
                    case 1:
                        listProduct = listProduct.OrderByDescending(x => x.ProductPrice).ToList();
                        break;
                    case 2:
                        listProduct = listProduct.OrderBy(x => x.ProductPrice).ToList();
                        break;
                    case 3:
                        listProduct = listProduct.OrderByDescending(x => x.CountComment).ToList();
                        break;
                    default:
                        listProduct = listProduct;
                        break;
                }
            }
            var category = await _categoryApiClient.GetAll();
            var topProduct = await _productApiClient.GetAll();
            topProduct = topProduct.Skip(0).Take(3).ToList();

            var ProductIndexViewModels = new ProductIndexModel()
            {
                TopProduct = topProduct,
                Category = category.ResultObj,
                ListProduct = listProduct,
            };
            return View(ProductIndexViewModels);
        }

        //public async Task<IActionResult> IndexFilter(int categoryId = 1, int S)
        //{
        //    var productRequest = new GetProductPagingRequest()
        //    {
        //        PageIndex = 1,
        //        PageSize = 6,
        //        keyword = "",
        //        CategoryId = categoryId
        //    };
        //    var product =await _productApiClient.GetPaging(productRequest);
        //    var listProduct = product.ResultObj.Items;
        //    if (SortOder != 0)
        //    {
        //        switch (SortOder)
        //        {
        //            case 1:
        //                listProduct = listProduct.OrderByDescending(x => x.ProductPrice).ToList();
        //                break;
        //            case 2:
        //                listProduct = listProduct.OrderBy(x => x.ProductPrice).ToList();
        //                break;
        //            case 3:
        //                listProduct = listProduct.OrderByDescending(x => x.CountComment).ToList();
        //                break;
        //            default:
        //                listProduct = listProduct;
        //                break;
        //        }
        //    }
        //    var category =await _categoryApiClient.GetAll();
        //    var topProduct = await _productApiClient.GetAll();
        //    topProduct = topProduct.Skip(0).Take(3).ToList();

        //    var ProductIndexViewModels = new ProductIndexModel()
        //    {
        //        TopProduct = topProduct,
        //        Category = category.ResultObj,
        //        ListProduct = listProduct,
        //    };
        //    return View(ProductIndexViewModels);
        //}
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
