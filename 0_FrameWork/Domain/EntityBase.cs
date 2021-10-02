using System;

namespace _0_FrameWork.Domain
{
    public class EntityBase
    {
        public EntityBase()
        {
            CreationDate = DateTime.Now;
        }

        public long Id { get; set; }
        public DateTime CreationDate { get; set; }
    }
}