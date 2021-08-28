using System.Collections.Generic;
using _0_FrameWork.Application;

namespace Shop.Management.Application.Contract.CourseGroup
{
    public interface ICourseGroupApplication
    {
        OperationResult Create(CreateCourseGroupViewModel command);
        OperationResult Edit(EditCourseGroupViewModel command);
        EditCourseGroupViewModel GetDetails(long id);
        List<CourseGroupViewModel> Search(CourseGroupSearchModel searchModel);
        OperationResult Remove(long id);
        OperationResult Restore(long id);

        List<CourseGroupViewModel> SelectList();
    }
}
