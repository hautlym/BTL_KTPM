using BTL_KTPM.Application.Catalog.Common;
using Microsoft.AspNetCore.Mvc;

namespace BTL_KTPM.Admin_App.Controllers.Component
{
    public class PagerViewComponent:ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(PageResultBase result)
        {
            return Task.FromResult((IViewComponentResult)View("Default", result));
        }
    }
}
