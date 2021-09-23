using System.Collections.Generic;
using _0_FrameWork.Application;
using _0_FrameWork.Domain.Infrastructure;
using AccountManagement.Application.Contract.Account;
using AccountManagement.Application.Contract.Role;
using AccountManagement.Domain.Account.Agg;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebHost.Areas.Administration.Pages.Account.Users
{
    public class EditModel : PageModel
    {

        private readonly IAccountApplication _application;
        private readonly IRoleApplication _role;
        public EditModel(IAccountApplication application, IRoleApplication role)
        {
            _application = application;
            _role = role;
        }

        public EditAccountViewModel Edit;

        [NeedPermission(Permission.EditUsers)]
        public void OnGet(long id)
        {
           
              Edit = _application.GetDetails(id);
            Edit.SelectList = _role.GetAll();

        }
        public IActionResult OnPost(EditAccountViewModel edit)
        {
            var post=  _application.Edit(edit);
            return RedirectToPage("Index");
        }

    }
}
