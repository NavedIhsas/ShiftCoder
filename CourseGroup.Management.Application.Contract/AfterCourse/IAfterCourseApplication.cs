using System.Collections.Generic;
using _0_FrameWork.Application;
using _0_FrameWork.Domain.Infrastructure;

namespace Shop.Management.Application.Contract.AfterCourse
{
    public interface IAfterCourseApplication
    {
        OperationResult Create(CreateAfterCourseViewModel command);
        OperationResult Edit(EditAfterCourseViewModel command);
        EditAfterCourseViewModel GetDetails(long id);
        List<AfterCourseViewModel> GetAll();
        OperationResult Remove(long id);
        OperationResult Restore(long id);
    }
}
