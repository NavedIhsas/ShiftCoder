using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Management.Application.Contract.Course;
using Shop.Management.Application.Contract.CourseSuitable;

namespace WebHost.Areas.Administration.Pages.Courses.CourseSuitable
{
    public class IndexModel : PageModel
    {
        private readonly ICourseSuitableApplication _application;
        private readonly ICourseApplication _course;
        public IndexModel(ICourseSuitableApplication application, ICourseApplication course)
        {
            _application = application;
            _course = course;
        }
        [TempData] public string Message { get; set; }
        public List<CourseSuitableViewModel> List;
        public void OnGet()
        {
            List = _application.GetAll();
        }

        public IActionResult OnGetCreate()
        {
            var select = new CreateSuitableViewModel()
            {
                SelectList = _course.SelectCourses(),
            };
            return Partial("./Create", select);
        }

        public JsonResult OnPostCreate(CreateSuitableViewModel command)
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

        public JsonResult OnPostEdit(EditSuitableViewModel command)
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
            return RedirectToPage("./Index");
        }

    }
}
