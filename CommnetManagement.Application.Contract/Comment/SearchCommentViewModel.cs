namespace CommentManagement.Application.Contract.Comment
{
    public class SearchCommentViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsConfirm { get; set; }
        public bool IsCancel { get; set; }
        public bool IsWaite { get; set; }

    }
}