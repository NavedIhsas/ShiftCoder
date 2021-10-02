using System.Collections.Generic;
using System.Linq;
using _0_FrameWork.Application;
using _0_FrameWork.Domain.Infrastructure;
using AccountManagement.Application.Contract.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebHost.Areas.Administration.Pages.Account.Roles
{
    public class EditModel : PageModel
    {
        private readonly IEnumerable<IPermissionExposer> _permissionExposers;
        private readonly IRoleApplication _role;
        public List<SelectListItem> Permissions = new();

        public EditRoleViewModel Role;

        public EditModel(IRoleApplication role, IEnumerable<IPermissionExposer> permissionExposers)
        {
            _role = role;
            _permissionExposers = permissionExposers;
        }

        [NeedPermission(Permission.EditRoles)]
        public void OnGet(long id)
        {
            Role = _role.GetDetails(id);

            var permissionDto = new List<PermissionDto>();

            foreach (var exposer in _permissionExposers)
            {
                var exposerPermission = exposer.Expose();
                foreach (var (key, value) in exposerPermission)
                {
                    permissionDto.AddRange(value);
                    var group = new SelectListGroup { Name = key };
                    foreach (var permission in value)
                    {
                        var item = new SelectListItem(permission.Name, permission.Code.ToString())
                        {
                            Group = group
                        };

                        if (Role.MapPermission.Any(x => x.Code == permission.Code))
                            item.Selected = true;

                        Permissions.Add(item);
                    }
                }
            }
        }

        [NeedPermission(Permission.EditRoles)]
        public IActionResult OnPost(EditRoleViewModel command)
        {
            var edit = _role.Edit(command);
            return RedirectToPage("Index");
        }
    }
}