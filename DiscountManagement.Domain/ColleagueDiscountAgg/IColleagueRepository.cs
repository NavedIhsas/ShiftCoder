using System.Collections.Generic;
using _0_FrameWork.Domain;
using ColleagueDiscountManagementApplication.Contract.ColleagueDiscount;

namespace DiscountManagement.Domain.ColleagueDiscountAgg
{
    public interface IColleagueRepository:IRepository<long, ColleagueDiscount>
    {
        EditColleagueDiscountViewModel GetDetails(long id);
        List<ColleagueDiscountViewModel> GetAllList();
    }
}