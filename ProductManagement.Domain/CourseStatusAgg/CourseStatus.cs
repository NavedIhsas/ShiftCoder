using System.Collections.Generic;
using ShopManagement.Domain.CourseAgg;

namespace ShopManagement.Domain.CourseStatusAgg
{
    public class CourseStatus
    {
        public CourseStatus(string title)
        {
            Title = title;
        }

        public CourseStatus()
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