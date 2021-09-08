using System.Collections.Generic;
using _0_FrameWork.Application;

namespace AccountManagement.Application.Contract.Role
{
    public interface IRoleApplication
    {
        OperationResult Create(CreateRoleViewModel command);
        OperationResult Edit(EditRoleViewModel command);
        EditRoleViewModel GetDetails(long id);
        List<RoleViewModel> GetAll();
    }
}