using System.Collections.Generic;
using _0_FrameWork.Domain.Infrastructure;

namespace AccountManagement.Application.Contract.Role
{
    public class EditRoleViewModel : CreateRoleViewModel
    {
        public long Id { get; set; }
        public List<PermissionDto> MapPermission { get; set; }

    }
}