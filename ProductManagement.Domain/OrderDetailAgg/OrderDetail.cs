using System.Collections.Generic;
using _0_FrameWork.Domain;
using ShopManagement.Domain.CourseAgg;
using ShopManagement.Domain.OrderAgg;

namespace ShopManagement.Domain.OrderDetailAgg
{
   public class OrderDetail:EntityBase
    {
        public long OrderId { get;private set; }
        public double Price { get; private set; }
        public long CourseId { get; private set; }
        public Order Order { get; private set; }
        public Course Course { get; private set; }
        public OrderDetail(  double price, long courseId, long orderId)
        {
            Price = price;
            CourseId = courseId;
            OrderId = orderId;
        }
    }
}
