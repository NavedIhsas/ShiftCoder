using _0_FrameWork.Domain;

namespace DiscountManagement.Domain.ColleagueDiscountAgg
{
    public class ColleagueDiscount : EntityBase
    {
        public ColleagueDiscount(long courseId, int discountRate)
        {
            CourseId = courseId;
            DiscountRate = discountRate;
            IsRemove = false;
        }

        public long CourseId { get; set; }
        public int DiscountRate { get; set; }
        public bool IsRemove { get; set; }

        public void Edit(long courseId, int discountRate)
        {
            CourseId = courseId;
            DiscountRate = discountRate;
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