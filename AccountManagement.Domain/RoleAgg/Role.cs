using System.Collections.Generic;
using _0_FrameWork.Domain;

namespace AccountManagement.Domain.RoleAgg
{
    public class Role : EntityBase
    {
        public Role()
        {
        }

        public Role(string name, List<Permission> permissions)
        {
            Name = name;
            Permissions = permissions;
            Accounts = new List<Account.Agg.Account>();
        }

        public string Name { get; private set; }
        public List<Account.Agg.Account> Accounts { get; private set; }
        public List<Permission> Permissions { get; private set; }

        public void Edit(string name, List<Permission> permissions)
        {
            Name = name;
            Permissions = permissions;
        }
    }
}