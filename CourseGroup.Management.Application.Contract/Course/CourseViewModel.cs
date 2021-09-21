namespace Shop.Management.Application.Contract.Course
{
    public class CourseViewModel
    {
        public string Name { get; set; }
        public string Picture { get; set; }
        public string Code { get; set; }
        public string Slug { get; set; }
        public long CourseGroupId { get; set; }
        public string UpdateDate { get; set; }
        public string CreationDate { get; set; }
        public string CourseGroup { get; set; }
        public double Price { get; set; }
        public string TeacherName { get; set; }
        public long Id { get; set; }
        public long TeacherId { get; set; }
    }
}