#nullable enable
using System.Collections.Generic;
using _0_FrameWork.Domain;
using Shop.Management.Application.Contract.OrderDetail;

namespace ShopManagement.Domain.OrderDetailAgg
{
  public interface IOrderDetailRepository:IRepository<long,OrderDetail>
  {

      List<OrderDetailViewModel> GetAllOrderDetail();
      OrderDetail? GetOrderDetailBy(long orderId, long courseId);
  }
}
