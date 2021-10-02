using System.Collections.Generic;
using System.Linq;
using _0_FrameWork.Domain;
using ShopManagement.Domain.OrderDetailAgg;

namespace ShopManagement.Domain.OrderAgg
{
    public class Order : EntityBase
    {
        public Order()
        {
        }

        public Order(long accountId, bool isFinally, double orderSum, List<OrderDetail> orderDetails)
        {
            AccountId = accountId;
            IsFinally = isFinally;
            OrderSum = orderSum;
            OrderDetails = orderDetails;
        }

        public long AccountId { get; private set; }
        public bool IsFinally { get; private set; }
        public double OrderSum { get; private set; }
        public List<OrderDetail> OrderDetails { get; private set; }

        public void ApplyDiscount(long orderId, double orderSum, int discountRate)
        {
            var percent = orderSum * discountRate / 100;
            OrderSum = orderSum - percent;
        }

        public void SumOrder(long orderId)
        {
            OrderSum = OrderDetails.Sum(x => x.Price);
        }

        public void Finally(long id)
        {
            IsFinally = true;
        }
    }
}