namespace DiscountManagement.Domain.UserDiscountAgg
{
    public class UserDiscount
    {
        public UserDiscount(long accountId, long discountCodeId)
        {
            AccountId = accountId;
            DiscountCodeId = discountCodeId;
        }

        public long AccountId { get; private set; }
        public long DiscountCodeId { get; private set; }
        public DiscountCode.DiscountCode DiscountCode { get; private set; }
    }
}