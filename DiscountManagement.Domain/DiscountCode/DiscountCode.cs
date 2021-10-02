using System;
using System.Collections.Generic;
using _0_FrameWork.Domain;
using DiscountManagement.Domain.UserDiscountAgg;

namespace DiscountManagement.Domain.DiscountCode
{
    public class DiscountCode : EntityBase
    {
        public DiscountCode()
        {
        }

        public DiscountCode(DateTime? startDate, DateTime? endDate, string reason, int? useableCount,
            string discountCode, int discountRate)
        {
            StartDate = startDate;
            EndDate = endDate;
            Reason = reason;
            UseableCount = useableCount;
            Code = discountCode;
            DiscountRate = discountRate;
        }

        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public string Reason { get; private set; }
        public int? UseableCount { get; private set; }
        public string Code { get; private set; }
        public int DiscountRate { get; private set; }
        public List<UserDiscount> UserDiscounts { get; private set; }

        public void UseCount(long id, int? useableCount)
        {
            UseableCount = useableCount -= 1;
        }

        public void Edit(DateTime? startDate, DateTime? endDate, string reason, int? useableCount, string discountCode,
            int discountRate)
        {
            StartDate = startDate;
            EndDate = endDate;
            Reason = reason;
            UseableCount = useableCount;
            Code = discountCode;
            DiscountRate = discountRate;
        }
    }
}