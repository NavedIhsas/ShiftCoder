using _0_FrameWork.Domain;
using ShopManagement.Domain.CourseAgg;

namespace ShopManagement.Domain.UserCoursesAgg
{
   public class UserCourse:EntityBase
    {
        public long AccountId { get; set; }
        public long CourseId { get; set; }
        public Course Course { get; set; }
    }
}
