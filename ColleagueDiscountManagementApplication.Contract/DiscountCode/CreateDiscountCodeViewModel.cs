using System;
using System.ComponentModel.DataAnnotations;
using _0_FrameWork.Application;

namespace ColleagueDiscountManagementApplication.Contract.DiscountCode
{
    public class CreateDiscountCodeViewModel
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Reason { get; set; }
        public int? UseableCount { get; set; }

        [Required(ErrorMessage = Validate.Required)]
        [MaxLength(50,ErrorMessage = Validate.MaxLength)]
        public string DiscountCode { get; set; }

        [Required(ErrorMessage = Validate.Required)]
        public int DiscountRate { get; set; }
    }
}