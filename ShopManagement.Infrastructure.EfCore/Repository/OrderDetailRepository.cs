using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_FrameWork.Domain.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Shop.Management.Application.Contract.OrderDetail;
using ShopManagement.Domain.OrderDetailAgg;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
  public class OrderDetailRepository:RepositoryBase<long,OrderDetail>,IOrderDetailRepository
  {
      private readonly ShopContext _context;

      public OrderDetailRepository(ShopContext dbContext, ShopContext context) : base(dbContext)
      {
          _context = context;
      }
     
      public List<OrderDetailViewModel> GetAllOrderDetail()
      {
          var order = _context.OrderDetails.ToList();
          return _context.OrderDetails.Include(x=>x.Course).Select(x => new OrderDetailViewModel
          {
              Price = x.Price.ToMoney(),
              CourseName = x.Course.Name
          }).ToList();
      }

      public OrderDetail GetOrderDetailBy(long orderId, long courseId)
      {
          var orderDetail= _context.OrderDetails.FirstOrDefault(x => x.OrderId == orderId && x.CourseId == courseId);
          return orderDetail ?? null;
      }
  }
}
