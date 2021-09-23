using Microsoft.AspNetCore.Mvc;

namespace WebHost.Pages.ViewComponent
{
    public class IndexDetailsViewComponent:Microsoft.AspNetCore.Mvc.ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("Default");
        }

    }
}
