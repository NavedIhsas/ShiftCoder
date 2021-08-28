using System.Collections.Generic;
using _0_FrameWork.Application;

namespace Shop.Management.Application.Contract.CourseStatus
{
    public interface ICourseStatusApplication
    {
        OperationResult Create(CourseStatusViewModel command);
        OperationResult Edit(EditCourseStatusViewModel command);
        EditCourseStatusViewModel GetDetails(long id);
        List<CourseStatusViewModel> GetAll();
        List<CourseStatusViewModel> SelectList();
    }
}