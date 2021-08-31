using System.Collections.Generic;
using ShopManagement.Domain.CourseAgg;

namespace ShopManagement.Domain.AfterTheCourseAgg
{
   public class AfterTheCourse
    {
        public long Id { get; private set; }
        public string Title { get; private set; }
        public bool IsRemove { get; private set; }
        public long CourseId { get;private set; }
        public Course Courses { get; private set; }

        public AfterTheCourse(string title, long courseId)
        {
            Title = title;
            CourseId = courseId;
            IsRemove = false;
        }

        public void Edit(string title, long courseId)
        {
            Title = title;
            CourseId = courseId;
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
