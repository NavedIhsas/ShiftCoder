using System.Collections.Generic;
using _0_FrameWork.Domain;
using CommentManagement.Application.Contract.Notification;

namespace CommentManagement.Domain.Notification.Agg
{
    public interface INotificationRepository : IRepository<long, Notification>
    {
        List<Notification> GetAllNotification(Notification search);
        List<Notification> GetFiveNotification();
      
    }
}