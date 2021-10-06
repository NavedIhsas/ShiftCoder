using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShiftCoderQuery.Contract.Course;
using ShiftCoderQuery.Contract.CourseGroup;

namespace WebHost.Pages
{
    public class CoursesModel : PageModel
    {
        private readonly ICourseQuery _course;

        public List<GetCourseGroupViewModel> Course;
        public CoursesModel(ICourseQuery course)
        {
            _course = course;
        }

        public void OnGet(CourseQuerySearchModel searchModel, string id)
        {
            Course = _course.GetCourseGroup(id);
        }
    }
}