using BTL_KTPM.ApiIntegration.Service.CartApiClient;
using BTL_KTPM.Application.Catalog.Carts.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BTL_KTPM.WebMvc.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartApiClient _cartApiClient;
        public CartController(ICartApiClient cartApiClient)
        {
            _cartApiClient = cartApiClient;
        }
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            var UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var Cart = await _cartApiClient.GetAllByUserId(Guid.Parse(UserId));
            
            return View(Cart);
        }
        public async Task<IActionResult> CheckOut(int CartId)
        {
            //if (!User.Identity.IsAuthenticated)
            //{
            //    return RedirectToAction("Login", "Account");
            //}
            //var UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            //var Cart = await _cartApiClient.GetAllByUserId(Guid.Parse(UserId));

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart(int ProductId, int Quantity)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            var Cart = new CreateCartRequest()
            {
                UserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value),
                ProductId = ProductId,
                Quantity = Quantity,
            };
            var kq =await _cartApiClient.CreateCart(Cart);
            if (kq.IsSuccessed)
            {
                return RedirectToAction("Index", "Cart");
            }
            ModelState.AddModelError("", "Thêm sản phẩm thất bại");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DeleteToCart(int CartId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            var kq = await _cartApiClient.Delete(CartId);
            if (kq)
            {
                return RedirectToAction("Index", "Cart");
            }
            ModelState.AddModelError("", "Thêm sản phẩm thất bại");
            return View();
        }
    }
}
