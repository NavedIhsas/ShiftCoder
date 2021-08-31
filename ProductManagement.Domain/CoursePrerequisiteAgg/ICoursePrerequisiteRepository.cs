using System.Collections.Generic;
using _0_FrameWork.Domain;
using _0_FrameWork.Domain.Infrastructure;
using Shop.Management.Application.Contract.CoursePrerequisite;

namespace ShopManagement.Domain.CoursePrerequisiteAgg
{
    public interface IPrerequisiteRepository:IRepository<long,CoursePrerequisite>
    {
        List<CoursePrerequisiteViewModel> GetAllList();
        EditCoursePrerequisiteViewModel GetDetails(long id);
        
    }
}