using System.Collections.Generic;
using _0_FrameWork.Domain;
using AccountManagement.Application.Contract.Role;

namespace AccountManagement.Domain.RoleAgg
{
    public interface IRoleRepository : IRepository<long, Role>
    {
        EditRoleViewModel GetDetails(long id);
        List<RoleViewModel> GetAllList();
        

    }
}