using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Management.Application.Contract.CourseGroup;

namespace WebHost.Areas.Administration.Pages.Courses.CourseGroup
{
    // ReSharper disable  StringLiteralTypo
    public class IndexModel : PageModel
    {
        private readonly ICourseGroupApplication _course;
        public IndexModel(ICourseGroupApplication course)
        {
            _course = course;
        }

        [TempData] public string Message { get; set; }
        public CourseGroupSearchModel SearchModel;
        public List<CourseGroupViewModel> CourseGroup;
        public void OnGet(CourseGroupSearchModel searchModel)
        {
            CourseGroup = _course.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            return Partial("./Create",new CreateCourseGroupViewModel());
        }

        public JsonResult OnPostCreate(CreateCourseGroupViewModel command)
        {
            var courseGroup = _course.Create(command);
            return new JsonResult(courseGroup);
        }


        public IActionResult OnGetEdit(long id)
        {
            var courseGroup = _course.GetDetails(id);
            return Partial("./Edit", courseGroup);
        }

        public JsonResult OnPostEdit(EditCourseGroupViewModel command)
        {
            var courseGroup = _course.Edit(command);
            return new JsonResult(courseGroup);
        }

        public IActionResult OnGetRemove(long id)
        {
            var removeCourseGroup = _course.Remove(id);
            if (removeCourseGroup.IsSucceeded)
                return RedirectToPage("./Index");

            Message  = "خطایی در عملیات رخ داده است";
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetRestore(long id)
        {
            var removeCourseGroup = _course.Restore(id);

            if (removeCourseGroup.IsSucceeded)
                return RedirectToPage("./Index");

            Message = "خطایی در عملیات رخ داده است";
            return RedirectToPage("./Index");
        }
    }
}
