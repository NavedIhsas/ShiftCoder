using Microsoft.AspNetCore.Mvc;
using ShiftCoderQuery.Contract.Course;

namespace WebHost.Pages.ViewComponent
{
    public class LatestCoursesViewComponent : Microsoft.AspNetCore.Mvc.ViewComponent
    {
        private readonly ICourseQuery _courseQuery;

        public LatestCoursesViewComponent(ICourseQuery courseQuery)
        {
            _courseQuery = courseQuery;
        }

        public IViewComponentResult Invoke()
        {
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            if (ipAddress == null) return null;
            var course = _courseQuery.LatestCourses();
            return View("Default", course);
        }
    }
}