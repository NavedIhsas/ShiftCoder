using System.ComponentModel.DataAnnotations;
using _0_FrameWork.Application;

namespace ShiftCoderQuery.Contract.Account
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = Validate.Required)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = Validate.Required)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = Validate.Required)]
        [Compare("NewPassword", ErrorMessage = "رمز عبور جدید با تکرار آن مطابقت ندارد")]
        public string ConfirmNewPassword { get; set; }

        public string Message { get; set; } = "this is a message";
        public UserInformationQueryModel UserInformation { get; set; }
        public long Id { get; set; }


    }
}