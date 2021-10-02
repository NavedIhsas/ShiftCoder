#nullable enable
using _0_FrameWork.Domain;

namespace ShopManagement.Domain.OrderDetailAgg
{
    public interface IOrderDetailRepository : IRepository<long, OrderDetail>
    {
        OrderDetail? GetOrderDetailBy(long orderId, long courseId);
    }
}