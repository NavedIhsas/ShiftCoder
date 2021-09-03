using System.Collections.Generic;
using _0_FrameWork.Domain;

namespace CommentManagement.Domain.CourseCommentAgg
{
   public class Comment:EntityBase
    {
        public string Name { get;private set; }
        public string Email { get; private set; }
        public string Message { get; private set; }
        public bool IsCanceled { get; private set; }
        public bool IsConfirmed { get; private set; }
        public long OwnerRecordId { get; private set; }
        public int Type { get; private set; }
        public long? ParentId { get; private set; }
        public Comment Parent { get; private set; }
        public List<Comment> Children { get; private set; }

        public Comment(string name, string email, string message, long ownerRecordId, int type, long? parentId)
        {
            Name = name;
            Email = email;
            Message = message;
            OwnerRecordId = ownerRecordId;
            Type = type;
            ParentId = parentId;
            IsConfirmed = false;
            IsCanceled = true;
        }

        public void IsConfirm(long id)
        {
            IsConfirmed = true;
            IsCanceled = false;
        }

        public void IsCancel(long id)
        {
            IsCanceled = true;
            IsConfirmed = false;
        }
    }
}
