using System.Collections.Generic;
using _0_FrameWork.Application;
using _0_FrameWork.Domain.Infrastructure;
using AccountManagement.Application.Contract.Account;
using AccountManagement.Domain.Account.Agg;
using AccountManagement.Infrastructure.EfCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebHost.Areas.Administration.Pages.Account.Users
{
    public class TeacherModel : PageModel
    {
        private readonly ITeacherRepository _teacher;
        private readonly IAccountApplication _account;

        public TeacherModel(ITeacherRepository teacher, IAccountApplication account)
        {
            _teacher = teacher;
            _account = account;
        }

        public List<TeacherViewModel> List;
        public EditTeacherViewModel Edit;


        [NeedPermission(Permission.ListTeacherAndBlogger)]
        public void OnGet()
        {
            List = _teacher.GetAllTeachers();
        }
      
       
        [NeedPermission(Permission.EditTeacherAndBlogger)]
        public IActionResult OnGetTeacherEdit(long id)
        {
            Edit = _teacher.GetTeacherDetails(id);
            return Partial("./TeacherEdit", Edit);
        }

        [NeedPermission(Permission.EditTeacherAndBlogger)]
        public JsonResult OnPostTeacherEdit(EditTeacherViewModel edit)
        {
            var teacher = _account.EditTeacher(edit);
            return new JsonResult(teacher);
        }

        [NeedPermission(Permission.DeleteTeacherAndBlogger)]
        public IActionResult OnGetDelete(long id)
        {
            var getTeacher = _teacher.GetTeacherBy(id);
            return Partial("DeleteTeacher",getTeacher);
        }
        public IActionResult OnGetConfirmDelete(long id)
        {
            _teacher.DeleteTeacher(id);
            return RedirectToPage("Teacher");
        }
    }
}
