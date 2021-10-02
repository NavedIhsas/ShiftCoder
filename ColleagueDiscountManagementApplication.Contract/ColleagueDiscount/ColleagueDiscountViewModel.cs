namespace ColleagueDiscountManagementApplication.Contract.ColleagueDiscount
{
    public class ColleagueDiscountViewModel
    {
        public string CourseName { get; set; }
        public int DiscountRate { get; set; }
        public string CreationDate { get; set; }
        public long CourseId { get; set; }
        public long Id { get; set; }
        public bool IsRemove { get; set; }
    }
}