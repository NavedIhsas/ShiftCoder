using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_FrameWork.Domain.Infrastructure;
using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.OrderDetailAgg;

namespace OrderInfrastructure.EfCore.Repository
{
  public class OrderDetailRepository:RepositoryBase<long,OrderDetail>,IOrderDetailRepository
  {
      private readonly OrderContext _context;
        public OrderDetailRepository(OrderContext dbContext, OrderContext context) : base(dbContext)
        {
            _context = context;
        }
    }
}
