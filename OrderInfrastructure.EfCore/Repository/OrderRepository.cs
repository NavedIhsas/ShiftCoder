using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_FrameWork.Domain.Infrastructure;
using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.OrderAgg;

namespace OrderInfrastructure.EfCore.Repository
{
   public class OrderRepository:RepositoryBase<long,Order>,IOrderRepository
   {
       private readonly OrderContext _context;
        public OrderRepository(OrderContext dbContext, OrderContext context) : base(dbContext)
        {
            _context = context;
        }
    }
}
