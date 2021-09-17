using System.Collections.Generic;
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
        public void OnGet()
        {
            List = _teacher.GetAllTeachers();
        }
      
       
        public IActionResult OnGetTeacherEdit(long id)
        {
            Edit = _teacher.GetTeacherDetails(id);
            return Partial("./TeacherEdit", Edit);
        }

        public JsonResult OnPostTeacherEdit(EditTeacherViewModel edit)
        {
            var teacher = _account.EditTeacher(edit);
            return new JsonResult(teacher);
        }

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
