using System.ComponentModel.DataAnnotations;
using _0_FrameWork.Domain;

namespace CommentManagement.Domain.HomePageDetailsAgg
{
   public class News: EntityBase
    {

        [Required]
        [MaxLength(500)]
        public string Title { get; set; }
    }
}
