using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftCoderQuery.Contract.Course
{
    public interface ICourseQuery
    {
        List<CourseQueryModel> GetAllCourse();
        List<CourseQueryModel> LatestCourses();


    }
}
