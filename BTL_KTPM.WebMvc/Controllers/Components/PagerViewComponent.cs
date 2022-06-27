using BTL_KTPM.Application.Catalog.Common;
using BTL_KTPM.WebMvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace BTL_KTPM.WebMvc.Controllers.Components
{
    public class PagerViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(ProductIndexModel result)
        {
            return Task.FromResult((IViewComponentResult)View("Default", result));
        }
    }
}
