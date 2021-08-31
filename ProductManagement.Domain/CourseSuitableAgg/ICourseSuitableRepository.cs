using System.Collections.Generic;
using _0_FrameWork.Domain;
using _0_FrameWork.Domain.Infrastructure;
using Shop.Management.Application.Contract.CourseSuitable;

namespace ShopManagement.Domain.CourseSuitableAgg
{
    public interface ICourseSuitableRepository:IRepository<long,CourseSuitable>
    {
        List<CourseSuitableViewModel> GetAllList();
        EditSuitableViewModel GetDetails(long id);


    }
}