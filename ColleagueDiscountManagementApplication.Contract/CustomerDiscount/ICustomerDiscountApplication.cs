using System.Collections.Generic;
using _0_FrameWork.Application;

namespace ColleagueDiscountManagementApplication.Contract.CustomerDiscount
{
    public interface ICustomerDiscountApplication
    {
        OperationResult Create(CreateCustomerDiscountViewModel command);
        OperationResult Edit(EditCustomerDiscountViewModel command);
        EditCustomerDiscountViewModel GetDetails(long id);
        List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel);
        OperationResult Remove(long id);
        OperationResult Restore(long id);
    }
}