using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShiftCoderQuery.Contract.Course;
using ShiftCoderQuery.Contract.CourseGroup;

namespace WebHost.Pages
{
    public class GetAllCoursesModel : PageModel
    {
        private readonly ICourseQuery _course;
        private readonly ICourseGroupQuery _group;

        public GetAllCoursesModel(ICourseQuery course, ICourseGroupQuery group)
        {
            _course = course;
            _group = group;
        }
        public CoursePaginationViewModel Course;
        public List<LatestCourseGroupViewModel> CourseGroups;
        public CourseQuerySearchModel SearchModel;
        public List<LatestCourseViewModel> LatestCourse;
        public void OnGet(CourseQuerySearchModel searchModel,  int pageId = 1)
        {
            Course = _course.GetAllCourse(searchModel, pageId);
            CourseGroups = _group.LatestCourseGroup();
            LatestCourse = _course.LatestCourses();

        }
    }
}
