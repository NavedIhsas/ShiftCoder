using Microsoft.AspNetCore.Mvc;
using ShiftCoderQuery.Contract.CourseGroup;

namespace WebHost.Pages.ViewComponent
{
    public class MenuViewComponent : Microsoft.AspNetCore.Mvc.ViewComponent
    {
        private readonly ICourseGroupQuery _courseGroup;

        public MenuViewComponent(ICourseGroupQuery courseGroup)
        {
            _courseGroup = courseGroup;
        }

        public IViewComponentResult Invoke()
        {
            var courseGroup = _courseGroup.GetAllCourseGroup();
            return View("Default", courseGroup);
        }
    }
}