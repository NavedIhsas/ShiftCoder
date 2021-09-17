using System.Collections.Generic;
using _0_FrameWork.Domain;
using ColleagueDiscountManagementApplication.Contract.DiscountCode;

namespace DiscountManagement.Domain.DiscountCode
{
    public interface IDiscountCodeRepository : IRepository<long, DiscountCode>
    {
        EditDiscountCodeViewModel GetDetails(long id);
        List<DiscountCodeViewModel> SearchModel(DiscountCodeSearchModel searchModel);
        DiscountCode GetDiscountBy(long id);
    }
}