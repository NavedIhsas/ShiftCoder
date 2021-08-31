using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using _0_FrameWork.Application;

namespace Shop.Management.Application.Contract.CourseGroup
{
    public class CreateCourseGroupViewModel
    {
        [Required(ErrorMessage = Validate.Required)]
       [MaxLength(200,ErrorMessage = Validate.MaxLength)]
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsRemove { get; set; }

        [Required(ErrorMessage = Validate.Required)]
        [MaxLength(100, ErrorMessage = Validate.MaxLength)]
        public string KeyWords { get; set; }

        [Required(ErrorMessage = Validate.Required)]
        [MaxLength(150, ErrorMessage = Validate.MaxLength)]
        public string MetaDescription { get; set; }

        [Required(ErrorMessage = Validate.Required)]
        [MaxLength(200, ErrorMessage = Validate.MaxLength)]
        public string Slug { get; set; }

        public long? SubGroupId { get; set; }
        public List<CourseGroupViewModel> CourseGroupSelectList { get; set; }
    }
}
