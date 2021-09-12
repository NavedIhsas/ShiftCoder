using System.Collections.Generic;
using _0_FrameWork.Application;

namespace ColleagueDiscountManagementApplication.Contract.DiscountCode
{
    public interface IDiscountCodeApplication
    {
        OperationResult Create(CreateDiscountCodeViewModel command);
        OperationResult Edit(EditDiscountCodeViewModel command);
        EditDiscountCodeViewModel GetDetails(long id);
        List<DiscountCodeViewModel> Search(DiscountCodeSearchModel searchModel);
    }
}
