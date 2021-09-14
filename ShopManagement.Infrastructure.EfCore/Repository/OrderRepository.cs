using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_FrameWork.Domain.Infrastructure;
using AccountManagement.Domain.Account.Agg;
using AccountManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using Shop.Management.Application.Contract.Order;
using Shop.Management.Application.Contract.OrderDetail;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.OrderDetailAgg;
using ShopManagement.Domain.UserCoursesAgg;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
    public class OrderRepository : RepositoryBase<long, Order>, IOrderRepository
    {
        private readonly ShopContext _context;
        private readonly IAccountRepository _account;
        public OrderRepository(ShopContext dbContext, ShopContext context, IAccountRepository account) : base(dbContext)
        {
            _context = context;
            _account = account;
        }

        public Order GetOrderById(long id)
            => _context.Orders.Find(id);

        public Order GetOrderBy(long accountId)
            => _context.Orders.FirstOrDefault(x => x.AccountId == accountId && !x.IsFinally);

        public OrderViewModel GetOrderUser(string email, long orderId)
        {
            var userId = _account.GetUserIdBy(email);

            return _context.Orders.Include(x => x.OrderDetails).ThenInclude(x => x.Course).Select(x => new OrderViewModel()
            {
                AccountId = x.AccountId,
                OrderSum = x.OrderSum,
                OrderDetails = MapOrderDetail(x.OrderDetails),
                IsFinally = x.IsFinally,
                Id = x.Id,
            }).FirstOrDefault(x => x.AccountId == userId && x.Id == orderId);

        }

        public List<OrderViewModel> GetAllOrderForAdminPanel()
        {
           
            return _context.Orders.Include(x => x.OrderDetails)
                .ThenInclude(x => x.Course).Where(x => x.IsFinally).Select(x => new OrderViewModel
                {
                    AccountId = x.AccountId,
                    IsFinally = x.IsFinally,
                    OrderSum = x.OrderSum,
                    Id = x.Id,
                    CreationDate = x.CreationDate.ToFarsi(),
                    OrderDetails = MapOrderDetail(x.OrderDetails),
                }).ToList();
        }

        public List<OrderViewModel> GetAllOrderUser(string email)
        {
            var userId = _account.GetUserIdBy(email);
            return _context.Orders.Include(x => x.OrderDetails)
                .ThenInclude(x => x.Course).Where(x => x.AccountId == userId).Select(x => new OrderViewModel
                {
                    AccountId = x.AccountId,
                    IsFinally = x.IsFinally,
                    OrderSum = x.OrderSum,
                    Id = x.Id,
                    CreationDate = x.CreationDate.ToFarsi(),
                    OrderDetails = MapOrderDetail(x.OrderDetails),
                }).ToList();
        }

        public Order Pay(string email, long orderId)
        {
            var userId = _account.GetUserIdBy(email);

            var order = _context.Orders.Include(x => x.OrderDetails).ThenInclude(x => x.Course)
                .FirstOrDefault(x => x.AccountId == userId && x.Id == orderId);
          return order ?? null;
        }


        public bool OrderFinally(string email, long orderId)
        {
            var userId = _account.GetUserIdBy(email);

            var order = _context.Orders.Include(x => x.OrderDetails).ThenInclude(x => x.Course)
                .FirstOrDefault(x => x.AccountId == userId && x.Id == orderId);

            if (order == null || order.IsFinally) return false;

            //TODO wallet management

            order.Finally(orderId);

            //Add course and user to UserCourse tbl
            foreach (var item in order.OrderDetails)
            {
                var user = _context.UserCourses.Any(x => x.AccountId == userId && x.CourseId == item.CourseId);
                if (user != false) return false;

                _context.UserCourses.Add(new UserCourse()
                {
                    AccountId = userId,
                    CourseId = item.CourseId
                });
            }
            _context.Update(order);
            _context.SaveChanges();
            return true;
        }

        private static List<OrderDetailViewModel> MapOrderDetail(IEnumerable<OrderDetail> orderDetails)
        {
            return orderDetails.Select(x => new OrderDetailViewModel()
            {
                CourseName = x.Course.Name,
                Picture = x.Course.Picture,
                DoublePrice = x.Price,
                CourseCode = x.Course.Code,
                Price = x.Price.ToMoney(),
            }).ToList();
        }
    }
}
