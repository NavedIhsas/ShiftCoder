using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using _0_FrameWork.Application;
using Shop.Management.Application.Contract.Course;

namespace ColleagueDiscountManagementApplication.Contract.ColleagueDiscount
{
    public class CreateColleagueDiscountViewModel
    {
        [Range(1, int.MaxValue, ErrorMessage = Validate.Required)]
        public long CourseId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = Validate.Required)]
        public int DiscountRate { get; set; }

        public List<CourseViewModel> SelectList { get; set; }
    }
}