using BTL_KTPM.Admin_App.Models;
using BTL_KTPM.ApiIntegration.Service.OrderApaiClient;
using BTL_KTPM.ApiIntegration.Service.ProductApiClient;
using BTL_KTPM.Application.Catalog.Orders;
using BTL_KTPM.Application.Catalog.Orders.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BTL_KTPM.Admin_App.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderApiClient _orderApiClient;
        private readonly IProductApiClient _productApiClient;
        public OrderController(IOrderApiClient orderApiClient, IProductApiClient productApiClient)
        {
            _orderApiClient = orderApiClient;
            _productApiClient = productApiClient;
        }
        public async Task<IActionResult> Index(string keyword, int PageIndex = 1, int PageSize = 5)
        {
            var request = new GetOrderRequest()
            {
                PageIndex = PageIndex,
                PageSize = PageSize,
                Keyword = keyword,
            };           
            var data = await _orderApiClient.GetAllPaging(request);
            ViewBag.Keyword = keyword;
            if (TempData["result"] != null)
            {
                ViewBag.SuscessMsg = TempData["result"];
            }
            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var result = await _orderApiClient.GetById(id);
            //var listProduct = _productApiClient.GetById(result.OrderDetails.Where(x=>x.ProductId))
            //var kq = new ModelViewOrderDetail()
            //{
            //    OrderModel = result,
            //    product = 
            //}
            return View(result);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(new DeleteOrderRequest
            {
                Id = id,
            });
        }
        [HttpPost]
        public async Task<IActionResult> Delete(DeleteOrderRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _orderApiClient.Delete(request.Id);
            if (result)
            {
                TempData["result"] = "hủy đơn thành công";
                return RedirectToAction("Index");

            }

            ModelState.AddModelError("", "hủy đơn không thành công");
            return View(request);
        }
    }
}
