using CommentManagement.Domain.Notification.Agg;
using Microsoft.AspNetCore.Mvc;

namespace WebHost.Areas.Administration.Pages.ViewComponent
{
    public class Notification:Microsoft.AspNetCore.Mvc.ViewComponent
    {
        private readonly INotificationRepository _notification;

        public Notification(INotificationRepository notification)
        {
            _notification = notification;
        }

        public IViewComponentResult Invoke()
        {
            var notification = _notification.GetFiveNotification();
            return View("Default", notification);
        }
    }
}
