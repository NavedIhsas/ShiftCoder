using System;
using _0_FrameWork.Domain;

namespace CommentManagement.Domain.VisitAgg
{
   public class Visit:EntityBase
    {
        public int Type { get;private set; }
        public string IpAddress { get; private set; }
        public long RecordOwnerId { get;private set; }
        public DateTime LastVisitDateTime { get; private set; }
        public int NumberOfVisit { get; private set; }

        public Visit(int type, string ipAddress, DateTime lastVisitDateTime, int numberOfVisit, long recordOwnerId)
        {
            Type = type;
            IpAddress = ipAddress;
            LastVisitDateTime = lastVisitDateTime;
            NumberOfVisit = numberOfVisit;
            RecordOwnerId = recordOwnerId;
        }

        public void ReduceVisit(int numberOfVisit)
        {
            NumberOfVisit = numberOfVisit + 1;
        }

        public void SetDateTime()
        {
            LastVisitDateTime = DateTime.Now;
        }
    }
}
