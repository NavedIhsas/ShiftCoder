using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReflectionIT.Mvc.Paging;
using ShiftCoderQuery.Contract.Course;
using ShiftCoderQuery.Contract.CourseGroup;

namespace WebHost.Pages
{
    public class CoursesModel : PageModel
    {
        private readonly ICourseQuery _course;
        private readonly ICourseGroupQuery _group;

        public CoursesModel(ICourseQuery course, ICourseGroupQuery group)
        {
            _course = course;
            _group = group;
        }

        public CoursePaginationViewModel Course;
        public List<CourseGroupQueryModel> CourseGroups;
        public CourseQuerySearchModel SearchModel;
        public void OnGet(CourseQuerySearchModel searchModel,List<long> groupId,int pageId=1)
        {
            Course = _course.GetAllCourse(searchModel,groupId,pageId);
            CourseGroups = _group.GetAllCourseGroup();
            ViewData["checked"] = groupId;
            ViewData["SearchModel"] = searchModel;

        }
    }
}
