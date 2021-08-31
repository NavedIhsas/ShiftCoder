using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_FrameWork.Application;
using _0_FrameWork.Domain.Infrastructure;

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
