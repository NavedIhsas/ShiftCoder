using System;

namespace ColleagueDiscountManagementApplication.Contract.CustomerDiscount
{
    public class CustomerDiscountViewModel
    {
        public long Id { get; set; }
        public int DiscountRate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string CourseName { get; set; }
        public long CourseId { get; set; }
        public bool IsRemove { get; set; }
        public string Reason { get; set; }
    }
}