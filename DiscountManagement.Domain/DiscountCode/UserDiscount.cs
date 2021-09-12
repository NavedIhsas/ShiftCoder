namespace DiscountManagement.Domain.DiscountCode
{
   public class UserDiscount
    {
        public long AccountId { get;private set; }
        public long DiscountCodeId { get; private set; }
        public DiscountCode DiscountCode { get; private set; }

        public UserDiscount(long accountId, long discountCodeId)
        {
            AccountId = accountId;
            DiscountCodeId = discountCodeId;
        }
    }
}
