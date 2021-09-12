using System.Collections.Generic;
using _0_FrameWork.Domain;
using Shop.Management.Application.Contract.Course;

namespace ShopManagement.Domain.CourseAgg
{
    public interface ICourseRepository:IRepository<long,Course>
    {
        EditCourseViewModel GetDetails(long id);
        List<CourseViewModel> Search(CourseSearchModel searchModel);
        List<CourseViewModel> SelectCourses();
        Course GetCourseBy(long courseId);
    }
}
