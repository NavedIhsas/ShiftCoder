using System.Collections.Generic;
using _0_FrameWork.Application;

namespace Shop.Management.Application.Contract.CourseLevel
{
    public interface ICourseLevelApplication
    {
        OperationResult Create(CreateCourseLevelViewModel command);
        OperationResult Edit(EditCourseLevelViewModel command);
        EditCourseLevelViewModel GetDetails(long id);
        List<CourseLevelViewModel> GetAll();
        List<CourseLevelViewModel> SelectList();
    }
}