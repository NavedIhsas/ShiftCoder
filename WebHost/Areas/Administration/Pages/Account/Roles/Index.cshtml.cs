using System.Collections.Generic;
using AccountManagement.Application.Contract.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebHost.Areas.Administration.Pages.Account.Roles
{
    public class IndexModel : PageModel
    {


        private readonly IRoleApplication _role;
        public IndexModel( IRoleApplication role)
        {
            _role = role;
        }
        public List<RoleViewModel> List;
        public void OnGet()
        {
            List = _role.GetAll();
        }

        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new CreateRoleViewModel());
        }

        public JsonResult OnPostCreate(CreateRoleViewModel command)
        {
            var create = _role.Create(command);
            return new JsonResult(create);
        }


        public IActionResult OnGetEdit(long id)
        {
            var getDetails = _role.GetDetails(id);
            return Partial("./Edit", getDetails);
        }
        public JsonResult OnPostEdit(EditRoleViewModel command)
        {
            var edit = _role.Edit(command);
            return new JsonResult(edit);
        }
    }
}
