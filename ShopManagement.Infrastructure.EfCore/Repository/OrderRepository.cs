using System;
using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_FrameWork.Application;
using _0_FrameWork.Domain.Infrastructure;
using AccountManagement.Domain.Account.Agg;
using CommentManagement.Domain.Notification.Agg;
using CommentManagement.Domain.VisitAgg;
using DiscountManagement.Domain.CustomerDiscountAgg;
using DiscountManagement.Domain.DiscountCode;
using DiscountManagement.Domain.UserDiscountAgg;
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
        private readonly IAccountRepository _account;
        private readonly ShopContext _context;
        private readonly ICustomerRepository _customerDiscount;
        private readonly IDiscountCodeRepository _discountCode;
        private readonly INotificationRepository _notification;
        private readonly IUserDiscountRepository _userDiscount;
        private readonly IVisitRepository _visit;

        public OrderRepository(ShopContext dbContext, ShopContext context, IAccountRepository account,
            IDiscountCodeRepository discountCode, IUserDiscountRepository userDiscount,
            ICustomerRepository customerDiscount, IVisitRepository visit,
            INotificationRepository notification) : base(dbContext)
        {
            _context = context;
            _account = account;
            _discountCode = discountCode;
            _userDiscount = userDiscount;
            _customerDiscount = customerDiscount;
            _visit = visit;
            _notification = notification;
        }

        public Order GetOrderById(long id)
        {
            return _context.Orders.Find(id);
        }

        public Order GetOrderBy(long accountId)
        {
            return _context.Orders.FirstOrDefault(x => x.AccountId == accountId && !x.IsFinally);
        }

        public OrderViewModel GetOrderUser(string email, long orderId)
        {
            var userId = _account.GetUserIdBy(email);

            return _context.Orders.Include(x => x.OrderDetails).ThenInclude(x => x.Course).Select(x =>
                new OrderViewModel
                {
                    AccountId = x.AccountId,
                    OrderSum = x.OrderSum,
                    OrderDetails = MapOrderDetail(x.OrderDetails),
                    IsFinally = x.IsFinally,
                    Id = x.Id
                }).FirstOrDefault(x => x.AccountId == userId && x.Id == orderId);
        }

        public List<OrderViewModel> GetAllOrderForAdminPanel(string ipAddress, string email)
        {
            var orders = _context.Orders.Include(x => x.OrderDetails)
                .ThenInclude(x => x.Course).Select(x => new OrderViewModel
                {
                    AccountId = x.AccountId,
                    IsFinally = x.IsFinally,
                    OrderSum = x.OrderSum,
                    Id = x.Id,
                    DateTimeDate = x.CreationDate,
                    CreationDate = x.CreationDate.ToFarsi(),
                    OrderDetails = MapOrderDetail(x.OrderDetails)
                }).ToList();
            foreach (var item in orders)
            {
                var user = _account.GetUserBy(item.AccountId);
                item.UserName = user.FullName;

                var userDiscount = _userDiscount.GetUserDiscountBy(item.AccountId);
                if (userDiscount == null) continue;
                var discountCode = _discountCode.GetDiscountBy(userDiscount.DiscountCodeId);
                if (discountCode == null) continue;
                item.DiscountRateCode = discountCode.DiscountRate;

                foreach (var customerDiscountRate in item.OrderDetails.Select(orderDetails =>
                    _customerDiscount.CustomerDiscountRate(orderDetails.CourseId)))
                    item.CustomerDiscountRate = customerDiscountRate;
            }

            var userId = _account.GetUserIdBy(email);

            //get visit by ipAddress and adminType
            var getVisit = _visit.GetVisitBy(ipAddress);
            if (getVisit == null)
            {
                var visit = new Visit(ThisType.AdminPanelIndex, ipAddress, DateTime.Now, 1, userId);
                _visit.Create(visit);
                _notification.GetNotificationBy(email);
                _visit.SaveChanges();
            }
            else if (getVisit.LastVisitDateTime.Hour != DateTime.Now.Hour)
            {
                var visit = new Visit(ThisType.AdminPanelIndex, ipAddress, DateTime.Now, 1, userId);
                _visit.Create(visit);
                _notification.GetNotificationBy(email);
                _visit.SaveChanges();
            }

            return orders;
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
                    OrderDetails = MapOrderDetail(x.OrderDetails)
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
                if (user) return false;

                _context.UserCourses.Add(new UserCourse
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
            var order = orderDetails.Select(x => new OrderDetailViewModel
            {
                CourseName = x.Course.Name.Substring(0, Math.Min(x.Course.Name.Length, 15)) + "...",
                Picture = x.Course.Picture,
                DoublePrice = x.Price,
                CourseCode = x.Course.Code,
                Price = x.Price,
                CourseSlug = x.Course.Slug,
                CourseId = x.CourseId
            }).ToList();
            return order;
        }
    }
}