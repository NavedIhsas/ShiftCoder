using System.Collections.Generic;
using AccountManagement.Application.Contract.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebHost.Areas.Administration.Pages.Account.Users
{
    public class TeacherModel : PageModel
    {
        private readonly IAccountApplication _account;

        public TeacherModel(IAccountApplication account)
        {
            _account = account;
        }

        public List<TeacherViewModel> List;
        public EditTeacherViewModel Edit;
        public void OnGet()
        {
            List = _account.GetAllTeachers();
        }
      
       
        public IActionResult OnGetTeacherEdit(long id)
        {
            Edit = _account.GetTeacherDetails(id);
            return Partial("./TeacherEdit", Edit);
        }

        public JsonResult OnPostTeacherEdit(EditTeacherViewModel edit)
        {
            var teacher = _account.EditTeacher(edit);
            return new JsonResult(teacher);
        }

      
    }
}
