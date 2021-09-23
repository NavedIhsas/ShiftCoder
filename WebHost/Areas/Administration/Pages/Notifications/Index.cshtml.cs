using System.Collections.Generic;
using _0_FrameWork.Application;
using _0_FrameWork.Domain.Infrastructure;
using AccountManagement.Domain.Account.Agg;
using CommentManagement.Application.Contract.Notification;
using CommentManagement.Domain.Notification.Agg;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebHost.Areas.Administration.Pages.Notifications
{
    public class IndexModel : PageModel
    {
        private readonly INotificationRepository _notification;
        private readonly IAccountRepository _account;
        public IndexModel(INotificationRepository notification, IAccountRepository account)
        {
            _notification = notification;
            _account = account;
        }

        public List<NotificationViewModel> List;
        public Notification SearchModel;
        public SelectList Types;
        public List<Notification> Notifications;

        [NeedPermission(Permission.SystemAdministratorNotification)]
        public void OnGet(Notification search)
        {
            var type = new List<int>() { 1, 2, 3, 4 };
          
           Types= new SelectList(type);
            List = _notification.GetAllNotification(search);
        }

        [NeedPermission(Permission.SystemAdministratorActivity)]
        public IActionResult OnGetAdminsActivity()
        {
            Notifications= _notification.GetActivityAdminsNotification();
            return Partial("AdminsActivity", Notifications);
        }
    }
}
