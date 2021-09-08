using System.ComponentModel.DataAnnotations;
using _0_FrameWork.Application;

namespace AccountManagement.Application.Contract.Account
{
   public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = Validate.Required)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = Validate.Required)]
        public string NewPassword { get; set; }
        public long Id { get; set; }
    }
}
