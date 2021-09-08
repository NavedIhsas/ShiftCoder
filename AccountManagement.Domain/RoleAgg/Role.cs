using System.Collections.Generic;
using _0_FrameWork.Domain;

namespace AccountManagement.Domain.RoleAgg
{
   public class Role:EntityBase
    {
        public string Name { get;private set; }
        public List<Account.Agg.Account> Accounts { get; private set; }

        public Role(string name)
        {
            Name = name;
        }

        public void Edit(string name)
        {
            Name = name;
        }

    }
}
