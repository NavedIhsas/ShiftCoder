using System.ComponentModel.DataAnnotations;
using _0_FrameWork.Application;

namespace AccountManagement.Application.Contract.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = Validate.Required)]
        public string Email { get; set; }

        [Required(ErrorMessage = Validate.Required)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string Message { get; set; }
    }
}