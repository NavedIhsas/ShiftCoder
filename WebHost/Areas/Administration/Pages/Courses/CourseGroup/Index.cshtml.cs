using System.Collections.Generic;
using _0_FrameWork.Application;
using _0_FrameWork.Domain.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        [NeedPermission(Permission.ListCourseGroups)]
        public void OnGet(CourseGroupSearchModel searchModel)
        {

            CourseGroup = _course.Search(searchModel);
        }


        public IActionResult OnGetCreate()
        {
            var selectGroup = new CreateCourseGroupViewModel()
            {
                CourseGroupSelectList = _course.SelectList()
            };
            return Partial("./Create", selectGroup);
        }

        [NeedPermission(Permission.CreateCourseGroups)]
        public JsonResult OnPostCreate(CreateCourseGroupViewModel command)
        {
            var courseGroup = _course.Create(command);
            return new JsonResult(courseGroup);
        }


        public IActionResult OnGetEdit(long id)
        {
            var courseGroup = _course.GetDetails(id);
            courseGroup.CourseGroupSelectList = _course.SelectList();
            return Partial("./Edit", courseGroup);
        }

        [NeedPermission(Permission.EditCourseGroups)]
        public JsonResult OnPostEdit(EditCourseGroupViewModel command)
        {
            var courseGroup = _course.Edit(command);
            return new JsonResult(courseGroup);
        }


        [NeedPermission(Permission.DeleteCourseGroups)]
        public IActionResult OnGetRemove(long id)
        {
            var removeCourseGroup = _course.Remove(id);
            if (removeCourseGroup.IsSucceeded)
                return RedirectToPage("./Index");

            Message = "خطایی در عملیات رخ داده است";
            return RedirectToPage("./Index");
        }

        [NeedPermission(Permission.RestoreCourseGroups)]
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
