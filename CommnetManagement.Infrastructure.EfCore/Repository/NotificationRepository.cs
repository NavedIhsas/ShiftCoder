using System;
using System.Collections.Generic;
using System.Linq;
using _0_FrameWork.Domain.Infrastructure;
using CommentManagement.Domain.Notification.Agg;

namespace CommentManagement.Infrastructure.EfCore.Repository
{
    public class NotificationRepository : RepositoryBase<long, Notification>, INotificationRepository
    {
        private readonly CommentContext _context;
        public NotificationRepository(CommentContext dbContext, CommentContext context) : base(dbContext)
        {
            _context = context;
        }


      
        public List<Notification> GetAllNotification(Notification search)
        {
            var notification = _context.Notifications.ToList();

            if (search.Type > 0)
                notification = notification.Where(x => x.Type == search.Type).ToList();

            var orderly = notification.OrderByDescending(x => x.CreationDate).ToList();
            return orderly;
        }

        public List<Notification> GetFiveNotification()
        {
           var notification= _context.Notifications.Where(x=>x.CreationDate.Date==DateTime.Today).
               OrderByDescending(x=>x.CreationDate).Take(10).ToList();
           return notification;
        }

    }
}
