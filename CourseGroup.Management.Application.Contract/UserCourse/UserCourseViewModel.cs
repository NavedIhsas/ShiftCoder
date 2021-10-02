namespace Shop.Management.Application.Contract.UserCourse
{
    public class UserCourseViewModel
    {
        public long AccountId { get; set; }
        public long CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseSlug { get; set; }
    }
}