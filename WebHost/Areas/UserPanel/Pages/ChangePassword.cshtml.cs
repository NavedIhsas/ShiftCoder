using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShiftCoderQuery.Contract.Account;

namespace WebHost.Areas.UserPanel.Pages.Account
{
    [Authorize]
    public class ChangePasswordModel : PageModel
    {
        private readonly IAccountQuery _user;

        public ChangePasswordModel(IAccountQuery user)
        {
            _user = user;
        }

        [BindProperty]
        public ChangePasswordViewModel ChangePassword { get; set; }

        public UserInformationQueryModel UserInformation { get; set; }
        public IActionResult OnGet()
        {
            UserInformation = _user.UserInformation(User.Identity.Name);
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return this.OnGet();

            var password = _user.ChangePassword(User.Identity.Name, ChangePassword);
            if (password == false)
            {
                ViewData["WrongPassword"] = true;
                return this.OnGet();
            }

            ViewData["isSuccess"] = true;
            return this.OnGet();
        }
    }
}
