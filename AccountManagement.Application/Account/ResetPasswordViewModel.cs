using System.ComponentModel.DataAnnotations;
using _0_FrameWork.Application;

namespace AccountManagement.Application.Contract.Account
{
    public class ResetPasswordViewModel
    {
        public string ActiveCode { get; set; }

        [Required(ErrorMessage = Validate.Required)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "رمز عبور با تکرار آن مطابقت ندارد")]
        [Required(ErrorMessage = Validate.Required)]
        public string RePassword { get; set; }
    }
}