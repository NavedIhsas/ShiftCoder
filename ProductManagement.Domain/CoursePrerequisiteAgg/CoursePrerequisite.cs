using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopManagement.Domain.CourseAgg;

namespace ShopManagement.Domain.CoursePrerequisiteAgg
{
   public class CoursePrerequisite
    {
        public long Id { get; private set; }
        public string Title { get; private set; }
        public bool IsRemove { get; private set; }
        public long CourseId { get; private set; }
        public Course Courses { get; private set; }

        public CoursePrerequisite(string title, long courseId)
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
