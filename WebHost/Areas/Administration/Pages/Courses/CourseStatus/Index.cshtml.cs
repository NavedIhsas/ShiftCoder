using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Management.Application.Contract.CourseGroup;
using Shop.Management.Application.Contract.CourseStatus;

namespace WebHost.Areas.Administration.Pages.Courses.CourseStatus
{
    public class IndexModel : PageModel
    {
        private readonly ICourseStatusApplication _course;
        public IndexModel(ICourseStatusApplication course)
        {
            _course = course;
        }

        public List<CourseStatusViewModel> CourseStatus;
        public void OnGet()
        {
            CourseStatus = _course.GetAll();
        }

        public IActionResult OnGetCreate()
        {
            return Partial("./Create",new CourseStatusViewModel());
        }

        public JsonResult OnPostCreate(CourseStatusViewModel command)
        {
            var courseStatus = _course.Create(command);
            return new JsonResult(courseStatus);
        }


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
