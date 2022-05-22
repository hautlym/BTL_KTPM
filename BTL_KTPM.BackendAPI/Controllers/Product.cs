using Microsoft.AspNetCore.Mvc;

namespace BTL_KTPM.BackendAPI.Controllers
{
    public class Product : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
