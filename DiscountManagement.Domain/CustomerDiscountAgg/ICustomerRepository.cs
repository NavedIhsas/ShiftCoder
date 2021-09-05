using System.Collections.Generic;
using _0_FrameWork.Domain;
using ColleagueDiscountManagementApplication.Contract.CustomerDiscount;

namespace DiscountManagement.Domain.CustomerDiscountAgg
{
    public interface ICustomerRepository:IRepository<long,CustomerDiscount>
    {
        EditCustomerDiscountViewModel GetDetails(long id);
        List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel);
    }
}