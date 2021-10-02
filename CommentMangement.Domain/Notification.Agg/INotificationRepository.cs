using System.Collections.Generic;
using _0_FrameWork.Domain;
using CommentManagement.Application.Contract.Notification;

namespace CommentManagement.Domain.Notification.Agg
{
    public interface INotificationRepository : IRepository<long, Notification>
    {
        List<NotificationViewModel> GetAllNotification(Notification search);
        List<Notification> GetFiveNotification();
        void GetNotificationBy(string email);
        List<Notification> GetActivityAdminsNotification();
    }
}