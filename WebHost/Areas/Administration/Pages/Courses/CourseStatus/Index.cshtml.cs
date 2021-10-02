using System.Collections.Generic;
using _0_FrameWork.Application;
using _0_FrameWork.Domain.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Management.Application.Contract.CourseStatus;

namespace WebHost.Areas.Administration.Pages.Courses.CourseStatus
{
    public class IndexModel : PageModel
    {
        private readonly ICourseStatusApplication _course;

        public List<CourseStatusViewModel> CourseStatus;

        public IndexModel(ICourseStatusApplication course)
        {
            _course = course;
        }

        public void OnGet()
        {
            CourseStatus = _course.GetAll();
        }

        [NeedPermission(Permission.CreateCourseStatus)]
        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new CourseStatusViewModel());
        }

        public JsonResult OnPostCreate(CourseStatusViewModel command)
        {
            var courseStatus = _course.Create(command);
            return new JsonResult(courseStatus);
        }


        [NeedPermission(Permission.EditCourseStatus)]
        public IActionResult OnGetEdit(long id)
        {
            var courseStatus = _course.GetDetails(id);
            return Partial("./Edit", courseStatus);
        }

        public JsonResult OnPostEdit(EditCourseStatusViewModel command)
        {
            var courseStatus = _course.Edit(command);
            return new JsonResult(courseStatus);
        }
    }
}