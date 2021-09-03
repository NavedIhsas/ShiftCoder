namespace CommentManagement.Application.Contract.Comment
{
    public class CreateCommentViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public bool IsCanceled { get; set; }
        public bool IsConfirmed { get; set; }
        public long OwnerRecordId { get; set; }
        public int Type { get; set; }

        public long? ParentId { get; set; }
    }
}