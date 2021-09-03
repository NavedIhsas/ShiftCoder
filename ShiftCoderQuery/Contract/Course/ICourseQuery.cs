using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShiftCoderQuery.Contract.Comment;

namespace ShiftCoderQuery.Contract.Course
{
    public interface ICourseQuery
    {
        List<CourseQueryModel> GetAllCourse(CourseQuerySearchModel searchQuery, List<long> groupId);
        List<CourseQueryModel> LatestCourses();
        CourseQueryModel GetCourseBySlug(string slug);


    }
}
