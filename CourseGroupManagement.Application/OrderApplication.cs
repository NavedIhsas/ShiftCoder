using System.Collections.Generic;
using _0_FrameWork.Application;
using AccountManagement.Domain.Account.Agg;
using CommentManagement.Domain.Notification.Agg;
using Shop.Management.Application.Contract.Order;
using ShopManagement.Domain.CourseAgg;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.OrderDetailAgg;

namespace ShopManagement.Application
{
    public class OrderApplication : IOrderApplication
    {
        private readonly IOrderRepository _order;
        private readonly IAccountRepository _account;
        private readonly ICourseRepository _course;
        private readonly IOrderDetailRepository _orderDetail;
        private readonly INotificationRepository _notification;

        public OrderApplication(IOrderRepository order, IAccountRepository account, ICourseRepository course, IOrderDetailRepository orderDetail, INotificationRepository notification)
        {
            _order = order;
            _account = account;
            _course = course;
            _orderDetail = orderDetail;
            _notification = notification;
        }

      
        public long Create(string userName, long courseId)
        {
          
            var userId = _account.GetUserIdBy(userName);
            var course = _course.GetCourseBy(courseId);
            var order = _order.GetOrderBy(userId);

            if (order == null)
            {
                order = new Order(userId, false, course.Price, new List<OrderDetail>()
                {
                    new OrderDetail(  course.Price, courseId,0),
                });
                _order.Create(order);
            }
            else
            {
                var detail = _orderDetail.GetOrderDetailBy(order.Id, courseId);

                if (detail == null)
                {
                    var orderDetail = new OrderDetail(course.Price, courseId, order.Id);
                    _orderDetail.Create(orderDetail);

                    //TODo fix this
                    order.SumOrder(order.Id);
                    _order.Update(order);
                }
                
            }
            _orderDetail.SaveChanges();
            _order.SaveChanges();

            var notification = new Notification($"کاربر سفارشی را برای ({course.Name}) ثبت کرد.", ThisType.Order,order.Id);
            _notification.Create(notification);
            _notification.SaveChanges();

            return order.Id;
        }
      
    }
}
