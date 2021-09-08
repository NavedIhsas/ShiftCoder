using System.Collections.Generic;
using AccountManagement.Application.Contract.Account;
using AccountManagement.Application.Contract.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebHost.Areas.Administration.Pages.Account.Users
{
    public class IndexModel : PageModel
    {


        private readonly IAccountApplication _application;
        private readonly IRoleApplication _role;
        public IndexModel(IAccountApplication application, IRoleApplication role)
        {
            _application = application;
            _role = role;
        }
        [TempData] public string Message { get; set; }
        public List<AccountViewModel> List;
        public AccountSearchModel SearchModel;
        public SelectList SelectList;
        public void OnGet(AccountSearchModel searchModel)
        {
            List = _application.Search(searchModel);
            SelectList = new SelectList(_role.GetAll(), "Id", "Name");
        }

        public IActionResult OnGetCreate()
        {
            var select = new RegisterUserViewModel()
            {
                SelectList = _role.GetAll(),
            };
            return Partial("./Create", select);
        }

        public JsonResult OnPostCreate(RegisterUserViewModel command)
        {
            var create = _application.Create(command);
            return new JsonResult(create);
        }


        public IActionResult OnGetEdit(long id)
        {
            var getDetails = _application.GetDetails(id);
            getDetails.SelectList = _role.GetAll();
            return Partial("./Edit", getDetails);
        }
        public JsonResult OnPostEdit(EditAccountViewModel command)
        {
            var edit = _application.Edit(command);
            return new JsonResult(edit);
        }

        public IActionResult OnGetChangePassword(long id)
        {
            var password = new ChangePasswordViewModel()
            {
                Id = id,
            };
            return Partial("./ChangePassword", password);
        }

        public JsonResult OnPostChangePassword(ChangePasswordViewModel change)
        {
            var password = _application.ChangePassword(change);
            return new JsonResult(password);
        }
        public IActionResult OnGetActive(long id)
        {
            var getDetails = _application.Active(id);

            if (getDetails.IsSucceeded)
                return RedirectToPage("./Index");
            Message = getDetails.Message;
            return RedirectToPage("./Index");
        }

        public IActionResult OnGetDeActive(long id)
        {
            var getDetails = _application.DeActive(id);

            if (getDetails.IsSucceeded)
                return RedirectToPage("./Index");
            Message = getDetails.Message;
            return  RedirectToPage("./Index");
        }

    }
}
