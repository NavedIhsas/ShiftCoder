using System.ComponentModel.DataAnnotations;
using _0_FrameWork.Application;

namespace AccountManagement.Application.Contract.Account
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = Validate.Required)]
        public string Email { get; set; }

        public string ActiveCode { get; set; }
    }
}