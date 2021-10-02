using System;
using _0_FrameWork.Domain;

namespace DiscountManagement.Domain.CustomerDiscountAgg
{
    public class CustomerDiscount : EntityBase
    {
        public CustomerDiscount(long courseId, int discountRate, string description, DateTime startTime,
            DateTime endTime, string reason)
        {
            CourseId = courseId;
            DiscountRate = discountRate;
            Description = description;
            StartTime = startTime;
            EndTime = endTime;
            Reason = reason;
            IsRemove = false;
        }

        public long CourseId { get; private set; }
        public int DiscountRate { get; private set; }
        public string Description { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public string Reason { get; private set; }
        public bool IsRemove { get; private set; }

        public void Edit(long courseId, int discountRate, string description, DateTime startTime, DateTime endTime,
            string reason)
        {
            CourseId = courseId;
            DiscountRate = discountRate;
            Description = description;
            StartTime = startTime;
            EndTime = endTime;
            Reason = reason;
        }

        public void Remove(long id)
        {
            IsRemove = true;
        }

        public void Restore(long id)
        {
            IsRemove = false;
        }
    }
}