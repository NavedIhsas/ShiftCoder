using System.Collections.Generic;

namespace ShiftCoderQuery.Contract.Comment
{
   public class CommentQueryModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public long Id { get; set; }
        public long? ParentId { get; set; }
        public string CreationDate { get; set; }
        public long OwnerRecordId { get; set; }
        public List<CommentQueryModel> SubComment { get; set; }
        public string ParentName { get; internal set; }
        public string Picture { get; set; }
        public CommentQueryModel()
        {
            SubComment = new List<CommentQueryModel>();
        }

    }
}
