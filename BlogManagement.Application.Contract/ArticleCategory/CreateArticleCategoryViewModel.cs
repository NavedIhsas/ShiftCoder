using System.ComponentModel.DataAnnotations;
using _0_FrameWork.Application;

namespace BlogManagement.Application.Contract.ArticleCategory
{
    public class CreateArticleCategoryViewModel
    {
        [Required(ErrorMessage = Validate.Required)]
        [MaxLength(250, ErrorMessage = Validate.MaxLength)]
        public string Name { get; set; }

        [Required(ErrorMessage = Validate.Required)]
        [MaxLength(100, ErrorMessage = Validate.MaxLength)]
        public string Keyword { get; set; }

        [Required(ErrorMessage = Validate.Required)]
        [MaxLength(150, ErrorMessage = Validate.MaxLength)]
        public string MetaDescription { get; set; }

        public int ShowOrder { get; set; }
        public string CanonicalAddress { get; set; }
    }
}