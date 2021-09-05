using Shop.Management.Application.Contract.Course;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using _0_FrameWork.Application;

namespace ColleagueDiscountManagementApplication.Contract.CustomerDiscount
{
    public class CreateCustomerDiscountViewModel
    {
        [Range(1, int.MaxValue, ErrorMessage = Validate.Required)]
        public long CourseId { get;  set; }

        [Range(1, int.MaxValue, ErrorMessage = Validate.Required)]
        public int DiscountRate { get;  set; }
        public string Description { get;  set; }

        [Required(ErrorMessage = Validate.Required)]
        public string StartTime { get;  set; }

        [Required(ErrorMessage = Validate.Required)]
        public string EndTime { get;  set; }
        public string Reason { get;  set; }
        public List<CourseViewModel> SelectList { get; set; }
    }
}