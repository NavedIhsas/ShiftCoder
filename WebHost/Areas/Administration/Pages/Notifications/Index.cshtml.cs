using System.Collections.Generic;
using CommentManagement.Domain.Notification.Agg;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebHost.Areas.Administration.Pages.Notifications
{
    public class IndexModel : PageModel
    {
        private readonly INotificationRepository _notification;

        public IndexModel(INotificationRepository notification)
        {
            _notification = notification;
        }

        public List<Notification> List;
        public Notification SearchModel;
        public SelectList Types;
        
        public void OnGet(Notification search)
        {
            var type = new List<int>() { 1, 2, 3, 4 };
          
           Types= new SelectList(type);
            List = _notification.GetAllNotification(search);
        }
    }
}
