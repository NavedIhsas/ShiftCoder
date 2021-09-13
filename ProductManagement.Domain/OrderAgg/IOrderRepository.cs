using System.Collections.Generic;
using _0_FrameWork.Domain;
using Shop.Management.Application.Contract.Order;

namespace ShopManagement.Domain.OrderAgg
{
   public interface IOrderRepository:IRepository<long,Order>
   {
       Order GetOrderBy(long accountId);
       Order GetOrderById(long id);
       OrderViewModel GetOrderUser(string email, long orderId);
       List<OrderViewModel> GetAllOrderUser(string email);
       bool OrderFinally(string email, long orderId);
       Order Pay(string email, long orderId);


   }
}
