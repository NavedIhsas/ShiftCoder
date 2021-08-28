using System.Collections.Generic;
using _0_FrameWork.Domain;
using Shop.Management.Application.Contract.CourseStatus;

namespace ShopManagement.Domain.CourseStatusAgg
{
    public interface ICourseStatusRepository:IRepository<long,CourseStatus>
    {
        EditCourseStatusViewModel GetDetails(long id);
        List<CourseStatusViewModel> GetAllCourseStatus();
        List<CourseStatusViewModel> SelectList();
    }
}
