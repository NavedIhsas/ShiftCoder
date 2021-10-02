using System;

namespace CommentManagement.Application.Contract.Notification
{
    public class NotificationViewModel
    {
        public string Text { get; set; }
        public int Type { get; set; }
        public DateTime CreationDate { get; set; }
    }
}