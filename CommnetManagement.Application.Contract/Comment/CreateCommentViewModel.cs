using System.ComponentModel.DataAnnotations;
using _0_FrameWork.Application;

namespace CommentManagement.Application.Contract.Comment
{
    public class CreateCommentViewModel
    {
        
        [Required(ErrorMessage = Validate.Required)]
        public string Name { get; set; }

        [Required(ErrorMessage = Validate.Required)]
        public string Email { get; set; }

        [Required(ErrorMessage = Validate.Required)]
        public string Message { get; set; }
        public bool IsCanceled { get; set; }
        public bool IsConfirmed { get; set; }
        public long OwnerRecordId { get; set; }
        public int Type { get; set; }

        public long? ParentId { get; set; }
    }
}