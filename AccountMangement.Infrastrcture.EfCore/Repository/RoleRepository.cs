using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_FrameWork.Domain.Infrastructure;
using AccountManagement.Application.Contract.Role;
using AccountManagement.Domain.RoleAgg;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EfCore.Repository
{
   public class RoleRepository:RepositoryBase<long,Role>,IRoleRepository
   {
       private readonly AccountContext _context;
        public RoleRepository(AccountContext dbContext, AccountContext context) : base(dbContext)
        {
            _context = context;
        }

        public EditRoleViewModel GetDetails(long id)
        {
            return _context.Roles.Select(x => new EditRoleViewModel
            {
                Name = x.Name,
                Id = x.Id
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<RoleViewModel> GetAllList()
        {
            return _context.Roles.Select(x => new RoleViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).OrderByDescending(x=>x.Id).ToList();

        }
    }
}
