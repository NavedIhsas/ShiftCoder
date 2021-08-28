using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_FrameWork.Application;
using Shop.Management.Application.Contract.CourseStatus;

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
