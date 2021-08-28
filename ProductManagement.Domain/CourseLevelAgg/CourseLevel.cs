using System.Collections.Generic;
using ShopManagement.Domain.CourseAgg;

namespace ShopManagement.Domain.CourseLevelAgg
{
    public class CourseLevel 
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public List<Course> Courses { get; private set; }

        public CourseLevel(string title)
        {
            Title = title;
        }

        public CourseLevel()
        {
            
        }
        public void Edit(string title)
        {
            Title = title;
        }
    }
}