using System.ComponentModel.DataAnnotations;
using _0_FrameWork.Application;

namespace Shop.Management.Application.Contract.CourseLevel
{
    public class CreateCourseLevelViewModel
    {
        [Required(ErrorMessage = Validate.Required)]
        [MaxLength(200, ErrorMessage = Validate.MaxLength)]
        public string Title { get; set; }
    }
}