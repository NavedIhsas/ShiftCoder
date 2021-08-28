using System.Collections.Generic;
using _0_FrameWork.Domain;
using Shop.Management.Application.Contract.CourseGroup;

namespace ShopManagement.Domain.CourseGroupAgg
{
    public interface ICourseGroupRepository:IRepository<long, ShopManagement.Domain.CourseGroupAgg.CourseGroup>
    {
        EditCourseGroupViewModel GetDetails(long id);
        List<CourseGroupViewModel> Search(CourseGroupSearchModel searchModel);
        List<CourseGroupViewModel> SelectList();
        string GetSlug(long id);
    }
}
