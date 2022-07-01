using BTL_KTPM.ApiIntegration.Service.ContactApiClient;
using BTL_KTPM.Application.Catalog.Contacts.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BTL_KTPM.Admin_App.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactApiClient _contactApiClient;
        public ContactController (IContactApiClient contactApiClient)
        {
           _contactApiClient = contactApiClient;
        }
        public async Task<IActionResult> Index()
        {
           
            var data = await _contactApiClient.GetAll();
            
            return View(data.ResultObj);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var result = await _contactApiClient.GetById(id);
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
            return View(new DeleteContactRequest
            {
                id = id,
            });
        }
        [HttpPost]
        public async Task<IActionResult> Delete(DeleteContactRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _contactApiClient.Delete(request.id);
            if (result)
            {
                TempData["result"] = "Xóa thành công";
                return RedirectToAction("Index");

            }

            ModelState.AddModelError("", "hủy đơn không thành công");
            return View(request);
        }
    }
}
