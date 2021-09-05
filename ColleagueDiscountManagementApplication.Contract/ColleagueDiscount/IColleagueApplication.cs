using System.Collections.Generic;
using _0_FrameWork.Application;

namespace ColleagueDiscountManagementApplication.Contract.ColleagueDiscount
{
    public interface IColleagueApplication
    {
        OperationResult Create(CreateColleagueDiscountViewModel command);
        OperationResult Edit(EditColleagueDiscountViewModel command);
        EditColleagueDiscountViewModel GetDetails(long id);
        List<ColleagueDiscountViewModel> GetAll();
        OperationResult Remove(long id);
        OperationResult Restore(long id);
    }
}