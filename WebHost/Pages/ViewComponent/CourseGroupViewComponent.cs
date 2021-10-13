using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ShiftCoderQuery.Contract.CourseGroup;

namespace WebHost.Pages.ViewComponent
{
    public class CourseGroupViewComponent : Microsoft.AspNetCore.Mvc.ViewComponent
    {
        private readonly ICourseGroupQuery _courseGroup;

        public CourseGroupViewComponent(ICourseGroupQuery courseGroup)
        {
            _courseGroup = courseGroup;
        }

        public IViewComponentResult Invoke()
        {
            var courseGroup = _courseGroup.LatestCourseGroup().Take(12);
            return View("Default", courseGroup);
        }
    }
}