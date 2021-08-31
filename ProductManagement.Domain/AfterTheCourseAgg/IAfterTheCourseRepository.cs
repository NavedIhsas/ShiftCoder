using System.Collections.Generic;
using System.Linq;
using _0_FrameWork.Domain;
using _0_FrameWork.Domain.Infrastructure;
using Shop.Management.Application.Contract.AfterCourse;

namespace ShopManagement.Domain.AfterTheCourseAgg
{
    public interface IAfterTheCourseRepository:IRepository<long,AfterTheCourse>
    {
        List<AfterCourseViewModel> GetAllList();
        EditAfterCourseViewModel GetDetails(long id);
    }
}