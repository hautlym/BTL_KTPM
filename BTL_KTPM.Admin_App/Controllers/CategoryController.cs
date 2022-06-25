using BTL_KTPM.ApiIntegration.Service.CategoryApiClient;
using BTL_KTPM.Application.Catalog.Categories;
using BTL_KTPM.Application.Catalog.Categories.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BTL_KTPM.Admin_App.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoriesApiClient _categoriesApiClient;

        public CategoryController(ICategoriesApiClient categoriesApiClient)
        {
            _categoriesApiClient = categoriesApiClient;
        }
        public async Task<IActionResult> Index(string keyword, int PageIndex = 1, int PageSize = 10)
        {
            var request = new GetCategoryRequest()
            {
                PageIndex = PageIndex,
                PageSize = PageSize,
                keyword = keyword,
            };
            
            var data = await _categoriesApiClient.GetAllPaging(request);
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
        public async Task<IActionResult> Create(CreateCategoryRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _categoriesApiClient.CreateCategory(request);
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
            var result = await _categoriesApiClient.GetById(id);
            if (result!=null)
            {
                var updateRequest = new UpdateCategoryRequest()
                {
                    Id = result.Id,
                    CategoryName  = result.CategoryName,
                    Description = result.Description,
                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateCategoryRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _categoriesApiClient.UpdateCategory(request.Id, request);
            if (result)
            {
                TempData["result"] = "Cập nhật thông tin thành công";
                return RedirectToAction("Index");

            }

            ModelState.AddModelError(""," Cập nhật không thành công");
            return View(request);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(new DeleteCategoryRequest()
            {
                id = id
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteCategoryRequest request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _categoriesApiClient.Delete(request.id);
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
            var result = await _categoriesApiClient.GetById(id);
            return View(result);
        }
        

    }
}
