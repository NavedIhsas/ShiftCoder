using System.Collections.Generic;
using ShopManagement.Domain.CourseAgg;

namespace ShopManagement.Domain.CourseLevelAgg
{
    public class CourseLevel
    {
        public CourseLevel(string title)
        {
            Title = title;
        }

        public CourseLevel()
        {
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public List<Course> Courses { get; private set; }

        public void Edit(string title)
        {
            Title = title;
        }
    }
}