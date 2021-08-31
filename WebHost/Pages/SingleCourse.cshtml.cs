using Microsoft.AspNetCore.Mvc.RazorPages;
using ShiftCoderQuery.Contract.Course;

namespace WebHost.Pages
{
    public class SingleCourseModel : PageModel
    {
        private readonly ICourseQuery _course;

        public SingleCourseModel(ICourseQuery course)
        {
            _course = course;
        }

        public CourseQueryModel Course;
        public void OnGet(string id)
        {
            Course = _course.GetCourseBySlug(id);
        }
    }
}
