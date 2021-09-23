using System.Collections.Generic;

namespace _0_FrameWork.Domain.Infrastructure
{
    public class PermissionDto
    {
        public string Name { get; set; }
        public int Code { get; set; }

        public PermissionDto(int code,string name)
        {
            Name = name;
            Code = code;
        }
    }

    public interface IPermissionExposer
    {
        Dictionary<string, List<PermissionDto>> Expose();
    }
}
