using System.Collections.Generic;
using _0_FrameWork.Application;

namespace Shop.Management.Application.Contract.CoursePrerequisite
{
    public interface ICoursePrerequisiteApplication
    {
        OperationResult Create(CreateCoursePrerequisiteViewModel command);
        OperationResult Edit(EditCoursePrerequisiteViewModel command);
        EditCoursePrerequisiteViewModel GetDetails(long id);
        List<CoursePrerequisiteViewModel> GetAll();
        OperationResult Remove(long id);
        OperationResult Restore(long id);
    }
}
