using System.Collections.Generic;
using _0_FrameWork.Domain;
using Shop.Management.Application.Contract.CourseLevel;

namespace ShopManagement.Domain.CourseLevelAgg
{
    public interface ICourseLevelRepository : IRepository<long, CourseLevel>
    {
        EditCourseLevelViewModel GetDetails(long id);
        List<CourseLevelViewModel> GetAllCourseLevel();
        List<CourseLevelViewModel> SelectList();
    }
}