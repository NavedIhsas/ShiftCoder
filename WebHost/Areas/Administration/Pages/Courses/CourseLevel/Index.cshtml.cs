using System.Collections.Generic;
using _0_FrameWork.Application;
using _0_FrameWork.Domain.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Management.Application.Contract.CourseLevel;

namespace WebHost.Areas.Administration.Pages.Courses.CourseLevel
{
    public class IndexModel : PageModel
    {
        private readonly ICourseLevelApplication _course;
        public IndexModel(ICourseLevelApplication course)
        {
            _course = course;
        }

        public List<CourseLevelViewModel> CourseLevel;

        public void OnGet()
        {
            CourseLevel = _course.GetAll();
        }

        [NeedPermission(Permission.CreateCourseLevel)]
        public IActionResult OnGetCreate()
        {
            return Partial("./Create",new CreateCourseLevelViewModel());
        }

        public JsonResult OnPostCreate(CreateCourseLevelViewModel command)
        {
            var courseLevel = _course.Create(command);
            return new JsonResult(courseLevel);
        }


        [NeedPermission(Permission.EditCourseLevel)]
        public IActionResult OnGetEdit(long id)
        {
            var courseLevel = _course.GetDetails(id);
            return Partial("./Edit", courseLevel);
        }

        public JsonResult OnPostEdit(EditCourseLevelViewModel command)
        {
            var courseLevel = _course.Edit(command);
            return new JsonResult(courseLevel);
        }

    }
}
