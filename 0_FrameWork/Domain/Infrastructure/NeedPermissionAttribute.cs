using System;

namespace _0_FrameWork.Domain.Infrastructure
{
    public partial class NeedPermissionAttribute : Attribute
    {
        public int Permission { get; set; }

        public NeedPermissionAttribute(int permission)
        {
            Permission = permission;
        }

    }

}