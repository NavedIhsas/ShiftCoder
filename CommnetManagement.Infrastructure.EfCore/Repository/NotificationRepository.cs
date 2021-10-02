using System;
using System.Collections.Generic;
using System.Linq;
using _0_FrameWork.Application;
using _0_FrameWork.Domain.Infrastructure;
using AccountManagement.Domain.Account.Agg;
using AccountManagement.Domain.RoleAgg;
using CommentManagement.Application.Contract.Notification;
using CommentManagement.Domain.Notification.Agg;

namespace CommentManagement.Infrastructure.EfCore.Repository
{
    public class NotificationRepository : RepositoryBase<long, Notification>, INotificationRepository
    {
        private readonly IAccountRepository _account;
        private readonly CommentContext _context;
        private readonly IRoleRepository _role;

        public NotificationRepository(CommentContext dbContext, CommentContext context, IAccountRepository aacouont,
            IRoleRepository role) : base(dbContext)
        {
            _context = context;
            _account = aacouont;
            _role = role;
        }

        public List<NotificationViewModel> GetAllNotification(Notification search)
        {
            var notification = _context.Notifications
                .Where(x => x.Type != ThisType.AdminPanelIndex)
                .Select(x => new NotificationViewModel
                {
                    Text = x.Text,
                    Type = x.Type,
                    CreationDate = x.CreationDate
                }).ToList();

            if (search.Type > 0)
                notification = notification.Where(x => x.Type == search.Type).ToList();

            var orderly = notification.OrderByDescending(x => x.CreationDate).ToList();
            return orderly;
        }

        public List<Notification> GetFiveNotification()
        {
            var notification = _context.Notifications.Where(x => x.CreationDate.Date == DateTime.Today)
                .Where(x => x.Type != ThisType.AdminPanelIndex).OrderByDescending(x => x.CreationDate).Take(10)
                .ToList();
            return notification;
        }

        public void GetNotificationBy(string email)
        {
            var userId = _account.GetUserIdBy(email);
            var user = _account.GetUserBy(userId);

            var role = _role.GetById(user.RoleId);

            var notification = new Notification($" {user.FullName} با نقش ({role.Name}) وارد پنل کاربری شد"
                , ThisType.AdminPanelIndex, user.Id);
            _context.Add(notification);
            _context.SaveChanges();
        }

        public List<Notification> GetActivityAdminsNotification()
        {
            return _context.Notifications.Where(x => x.Type == ThisType.AdminPanelIndex).ToList();
        }
    }
}