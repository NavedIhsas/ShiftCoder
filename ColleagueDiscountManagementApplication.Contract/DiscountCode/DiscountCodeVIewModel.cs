using System;

namespace ColleagueDiscountManagementApplication.Contract.DiscountCode
{
    public class DiscountCodeViewModel
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Reason { get; set; }
        public string DiscountCode { get; set; }
        public int DiscountRate { get; set; }
        public int? UseableCount { get; set; }
        public long Id { get; set; }
    }
}