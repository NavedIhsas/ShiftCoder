using _0_FrameWork.Domain;

namespace DiscountManagement.Domain.UserDiscountAgg
{
    public interface IUserDiscountRepository : IRepository<long, UserDiscount>
    {
        UserDiscount GetUserDiscountBy(long accountId);
    }
}