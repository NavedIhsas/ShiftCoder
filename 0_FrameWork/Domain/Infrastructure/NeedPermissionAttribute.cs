using System;

namespace _0_FrameWork.Domain.Infrastructure
{
    public class NeedPermissionAttribute : Attribute
    {
        public NeedPermissionAttribute(int permission)
        {
            Permission = permission;
        }

        public int Permission { get; set; }
    }
}