using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShiftCoderQuery.Contract.Course;

namespace WebHost.Pages
{
    public class CoursesModel : PageModel
    {
        private readonly ICourseQuery _course;

        public CoursesModel(ICourseQuery course)
        {
            _course = course;
        }

        public List<CourseQueryModel> Courses;
        public CourseQuerySearchModel SearchModel;
        public void OnGet(CourseQuerySearchModel searchModel)
        {
            Courses = _course.GetAllCourse(searchModel);
          
        }
    }
}
