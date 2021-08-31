using System.Collections.Generic;
using _0_FrameWork.Domain.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Management.Application.Contract.AfterCourse;
using Shop.Management.Application.Contract.Course;
using Shop.Management.Application.Contract.CoursePrerequisite;
using Shop.Management.Application.Contract.CourseSuitable;

namespace WebHost.Areas.Administration.Pages.Courses.CoursePrerequisite
{
    public class IndexModel : PageModel
    {


        private readonly ICoursePrerequisiteApplication _application;
        private readonly ICourseApplication _course;
        public IndexModel(ICoursePrerequisiteApplication application, ICourseApplication course)
        {
            _application = application;
            _course = course;
        }
        [TempData] public string Message { get; set; }
        public List<CoursePrerequisiteViewModel> List;
        public void OnGet()
        {
            List = _application.GetAll();
        }

        public IActionResult OnGetCreate()
        {
            var select = new CreateCoursePrerequisiteViewModel()
            {
                SelectList = _course.SelectCourses(),
            };
            return Partial("./Create", select);
        }

        public JsonResult OnPostCreate(CreateCoursePrerequisiteViewModel command)
        {
            var create = _application.Create(command);
            return new JsonResult(create);
        }


        public IActionResult OnGetEdit(long id)
        {
            var getDetails = _application.GetDetails(id);
            getDetails.SelectList = _course.SelectCourses();
            return Partial("./Edit", getDetails);
        }
        public JsonResult OnPostEdit(EditCoursePrerequisiteViewModel command)
        {
            var edit = _application.Edit(command);
            return new JsonResult(edit);
        }

        public IActionResult OnGetRemove(long id)
        {
            var getDetails = _application.Remove(id);

            if (getDetails.IsSucceeded)
                return RedirectToPage("./Index");
            Message = getDetails.Message;
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetRestore(long id)
        {
            var getDetails = _application.Restore(id);

            if (getDetails.IsSucceeded)
                return RedirectToPage("./Index");
            Message = getDetails.Message;
            return  RedirectToPage("./Index");
        }

    }
}
