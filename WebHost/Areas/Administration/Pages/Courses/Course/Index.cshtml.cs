using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shop.Management.Application.Contract.Course;
using Shop.Management.Application.Contract.CourseGroup;
using Shop.Management.Application.Contract.CourseLevel;
using Shop.Management.Application.Contract.CourseStatus;

namespace WebHost.Areas.Administration.Pages.Courses.Course
{
    public class IndexModel : PageModel
    {
        private readonly ICourseApplication _course;
        private readonly ICourseGroupApplication _courseGroup;
        private readonly ICourseLevelApplication _courseLevel;
        private readonly ICourseStatusApplication _courseStatus;

        public IndexModel(ICourseApplication course, ICourseGroupApplication courseGroup, ICourseStatusApplication courseStatus, ICourseLevelApplication courseLevel)
        {
            _course = course;
            _courseGroup = courseGroup;
            _courseStatus = courseStatus;
            _courseLevel = courseLevel;
        }

        public SelectList SelectList;
        public CourseSearchModel SearchModel;
        public List<CourseViewModel> Course;
        public void OnGet(CourseSearchModel searchModel)
        {
            SelectList = new SelectList(_courseGroup.SelectList(), "Id", "Title");
            Course = _course.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var course = new CreateCourseViewModel()
            {
                CourseGroupSelectList = _courseGroup.SelectList(),
                CourseStatusSelectList=_courseStatus.SelectList(),
                CourseLevelSelectList = _courseLevel.SelectList(),
            };
            return Partial("./Create", course);
        }

        public JsonResult OnPostCreate(CreateCourseViewModel command)
        {
            var course = _course.Create(command);
            return new JsonResult(course);
        }

        public IActionResult OnGetEdit(long id)
        {
            var course = _course.GetDetails(id);

            course.CourseStatusSelectList = _courseStatus.SelectList();
            course.CourseLevelSelectList = _courseLevel.SelectList();
            course.CourseGroupSelectList = _courseGroup.SelectList();

            return Partial("./Edit", course);

        }

        public JsonResult OnPostEdit(EditCourseViewModel command)
        {
            var course = _course.Edit(command);
            return new JsonResult(course);
        }
    }
}
