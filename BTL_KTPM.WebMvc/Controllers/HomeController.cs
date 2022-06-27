using BTL_KTPM.ApiIntegration.Service.ContactApiClient;
using BTL_KTPM.ApiIntegration.Service.ProductApiClient;
using BTL_KTPM.Application.Catalog.Contacts.Dtos;
using BTL_KTPM.WebMvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BTL_KTPM.WebMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductApiClient _productApiClient;
        private readonly IContactApiClient _contactApiClient;
        public HomeController(ILogger<HomeController> logger, IProductApiClient productApiClient, IContactApiClient contactApiClient)
        {
            _logger = logger;
            _productApiClient = productApiClient;
            _contactApiClient = contactApiClient;
        }

        public async Task<IActionResult> Index()
        {
            var Products =await _productApiClient.GetAll();
            var HomeViewModel = new HomeViewModels()
            {
                NewsProduct = Products.Where(x=>x.IsNewProduct==true).Skip(0).Take(4).ToList(),
                DiscountProduct = Products.Where(x=>x.Discount!=0).Skip(0).Take(4).ToList(),
                PopularProduct = Products.OrderByDescending(x=>x.CountComment).Skip(0).Take(4).ToList(),
                ProductInShop = Products.Skip(0).Take(8).ToList(),
            };
            return View(HomeViewModel);
        }
        public  IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Contact(CreateContactRequest request)
        {
            var kq =await _contactApiClient.CreateContact(request);
            if (kq.IsSuccessed)
            {
                return View();
            }
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}