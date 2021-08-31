using System.Collections.Generic;
using _0_FrameWork.Application;
using _0_FrameWork.Domain.Infrastructure;

namespace Shop.Management.Application.Contract.CourseSuitable
{
    public interface ICourseSuitableApplication
    {
        OperationResult Create(CreateSuitableViewModel command);
        OperationResult Edit(EditSuitableViewModel command);
        EditSuitableViewModel GetDetails(long id);
        List<CourseSuitableViewModel> GetAll();
        OperationResult Remove(long id);
        OperationResult Restore(long id);
     
    }
}
