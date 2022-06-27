using BTL_KTPM.ApiIntegration.Service.CartApiClient;
using BTL_KTPM.ApiIntegration.Service.OrderApaiClient;
using BTL_KTPM.Application.Catalog.Carts.Dtos;
using BTL_KTPM.Application.Catalog.Orders.Dtos;
using BTL_KTPM.Application.Catalog.Products.Dtos;
using BTL_KTPM.WebMvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BTL_KTPM.WebMvc.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartApiClient _cartApiClient;
        private readonly IOrderApiClient _orderApiClient;
        public CartController(ICartApiClient cartApiClient, IOrderApiClient orderApiClient)
        {
            _cartApiClient = cartApiClient;
            _orderApiClient = orderApiClient;
        }
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            var UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var Cart = await _cartApiClient.GetAllByUserId(Guid.Parse(UserId));

            if (Cart == null)
            {
                return View();
            }
            return View(Cart);
        }
        public async Task<IActionResult> CheckOutView()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            var UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var Cart = await _cartApiClient.GetAllByUserId(Guid.Parse(UserId));

            return View(Cart);
        }

        [HttpPost]
        public async Task<IActionResult> CheckOut(CheckOutRequest request)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            var UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var Cart = await _cartApiClient.GetAllByUserId(Guid.Parse(UserId));
            if (Cart.Count < 0)
            {
                return RedirectToAction("CheckOutView", "Cart");
            }
            var order = new CreateOrderRequest()
            {
                ShipAddress = request.ShipAddress,
                ShipDescription = request.ShipDescription,
                ShipEmail = request.ShipEmail,
                ShipName = request.ShipName,
                ShipNumberPhone = request.ShipNumberPhone,
                AppUserId = Guid.Parse(UserId),
            };
            var kq = await _orderApiClient.CreateOrder(order);
            var order2 = await _orderApiClient.GetAllByUserId(Guid.Parse(UserId));
            var kq2 = await _orderApiClient.DeleteCart(Guid.Parse(UserId));
            //var order3 = await _orderApiClient.GetAllByUserId(Guid.Parse(UserId));
            if (kq.IsSuccessed)
            {
                _orderApiClient.DeleteCart(Guid.Parse(UserId));
                return RedirectToAction("CheckOutDetail", "Cart");
            }
            return RedirectToAction("CheckOutView", "Cart");
        }
        public async Task<IActionResult> CheckOutDetail()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            var UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var Order = await _orderApiClient.GetAllByUserId(Guid.Parse(UserId));
            return View(Order);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteCheckOut(int OrderId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            var kq = await _orderApiClient.Delete(OrderId);
            if (kq)
            {
                return RedirectToAction("CheckOutDetail", "Cart");
            }
            var UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var Order = await _orderApiClient.GetAllByUserId(Guid.Parse(UserId));
            return View(Order);
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
            var kq = await _cartApiClient.CreateCart(Cart);
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
