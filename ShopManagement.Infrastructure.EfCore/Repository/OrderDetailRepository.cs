using System.Linq;
using _0_FrameWork.Domain.Infrastructure;
using ShopManagement.Domain.OrderDetailAgg;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
    public class OrderDetailRepository : RepositoryBase<long, OrderDetail>, IOrderDetailRepository
    {
        private readonly ShopContext _context;

        public OrderDetailRepository(ShopContext dbContext, ShopContext context) : base(dbContext)
        {
            _context = context;
        }


        public OrderDetail GetOrderDetailBy(long orderId, long courseId)
        {
            var orderDetail = _context.OrderDetails.FirstOrDefault(x => x.OrderId == orderId && x.CourseId == courseId);
            return orderDetail ?? null;
        }
    }
}