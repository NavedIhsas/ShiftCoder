using System.Collections.Generic;
using System.Linq;
using _0_FrameWork.Domain.Infrastructure;
using AccountManagement.Application.Contract.Role;
using AccountManagement.Domain.RoleAgg;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EfCore.Repository
{
    public class RoleRepository : RepositoryBase<long, Role>, IRoleRepository
    {
        private readonly AccountContext _context;

        public RoleRepository(AccountContext dbContext, AccountContext context) : base(dbContext)
        {
            _context = context;
        }

        public EditRoleViewModel GetDetails(long id)
        {
            var role = _context.Roles.Select(x => new EditRoleViewModel
            {
                Name = x.Name,
                Id = x.Id,
                MapPermission = MapPermission(x.Permissions)
            }).AsNoTracking().FirstOrDefault(x => x.Id == id);

            role.Permissions = role.MapPermission.Select(x => x.Code).ToList();

            return role;
        }

        public List<RoleViewModel> GetAllList()
        {
            return _context.Roles.Select(x => new RoleViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).OrderByDescending(x => x.Id).ToList();
        }

        private static List<PermissionDto> MapPermission(IEnumerable<Permission> permissions)
        {
            return permissions.Select(x => new PermissionDto(x.Code, x.Name)).ToList();
        }
    }
}