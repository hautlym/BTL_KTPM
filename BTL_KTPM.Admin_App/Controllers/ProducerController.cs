using BTL_KTPM.Admin_App.Service.ProducerApiClient;
using BTL_KTPM.Application.Catalog.Producers.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BTL_KTPM.Admin_App.Controllers
{
    public class ProducerController : BaseController
    {
        private readonly IManageProducerApiClient _manageProducerApiClient;

        public ProducerController(IManageProducerApiClient manageProducerApiClient)
        {
            _manageProducerApiClient = manageProducerApiClient;
        }

        public async Task<IActionResult> Index(string keyword, int PageIndex = 1, int PageSize = 10)
        {
            var request = new GetProducerRequest()
            {
                PageIndex = PageIndex,
                PageSize = PageSize,
                keyword = keyword,
            };

            var data = await _manageProducerApiClient.GetAllPaging(request);
            ViewBag.Keyword = keyword;
            if (TempData["result"] != null)
            {
                ViewBag.SuscessMsg = TempData["result"];
            }
            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProducerRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _manageProducerApiClient.CreateProducer(request);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Thêm tài khoản thành công";
                return RedirectToAction("Index");
            }
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _manageProducerApiClient.GetById(id);
            if (result != null)
            {
                var updateRequest = new UpdateProducerRequest()
                {
                    id = id,
                    ProducerName = result.ProducerName,
                    Description = result.Description,
                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateProducerRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _manageProducerApiClient.UpdateProducer(request.id, request);
            if (result)
            {
                TempData["result"] = "Cập nhật thông tin thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", " Cập nhật không thành công");
            return View(request);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(new DeleteProducerRequest()
            {
                Id = id
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteProducerRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _manageProducerApiClient.Delete(request.Id);
            if (result)
            {
                TempData["result"] = "Xóa tài khoản thành công";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Xóa không thành công");
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var result = await _manageProducerApiClient.GetById(id);
            return View(result);
        }
    }
}