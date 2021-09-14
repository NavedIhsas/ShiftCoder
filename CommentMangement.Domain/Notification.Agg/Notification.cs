using System;
using System.ComponentModel.DataAnnotations;
using _0_FrameWork.Domain;

namespace CommentManagement.Domain.Notification.Agg
{
   public class Notification:EntityBase
    {
        [MaxLength(250)]
        public string Text { get; private set; }
        public int Type { get;private set; }
        public long RecordOwnerId { get; private set; }
        public Notification()
        {
            
        }
        public Notification(string text, int type, long recordOwnerId)
        {
            Text = text;
            Type = type;
            RecordOwnerId = recordOwnerId;
        }
        
    }
}
