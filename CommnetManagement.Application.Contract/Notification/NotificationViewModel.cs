using System;

namespace CommentManagement.Application.Contract.Notification
{
   public class NotificationViewModel
    {
        public string Text { get; set; }
        public TimeSpan CreationDate { get; set; }
    }
}
