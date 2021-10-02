using ShiftCoderQuery.Contract.Discount.Enum;

namespace ShiftCoderQuery.Contract.Discount
{
    public interface IDiscountQuery
    {
        DiscountUseType UseDiscount(long orderId, string code);
        bool? GetAllUserDiscount(long accountId, long discountCodeId);
    }
}