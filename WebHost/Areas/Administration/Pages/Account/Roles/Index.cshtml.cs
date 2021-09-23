using System.Collections.Generic;
using _0_FrameWork.Application;
using _0_FrameWork.Domain.Infrastructure;
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

      [NeedPermission(Permission.ListRoles)]
        public void OnGet()
        {
            List = _role.GetAll();
        }

    }
}
