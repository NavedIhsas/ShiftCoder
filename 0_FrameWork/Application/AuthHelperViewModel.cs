using System.Collections.Generic;

namespace _0_FrameWork.Application
{
    public class AuthHelperViewModel
    {
        public AuthHelperViewModel(long accountId, long roleId, string fullname, string email, List<int> permissions)
        {
            AccountId = accountId;
            RoleId = roleId;
            Fullname = fullname;
            Email = email;
            Permissions = permissions;
        }

        public long AccountId { get; set; }
        public long RoleId { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public List<int> Permissions { get; set; }
    }
}