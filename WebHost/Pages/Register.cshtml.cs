using AccountManagement.Application.Contract.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebHost.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IAccountApplication _account;

        public RegisterModel(IAccountApplication account)
        {
            _account = account;
        }

        [TempData] public string Message { get; set; }

        [BindProperty]
        public RegisterUserViewModel Register { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost(RegisterUserViewModel command)
        {
            if (!ModelState.IsValid) return Page();

            var register = _account.Create(command);
            if (register.IsSucceeded)
            {
                ViewData["IsSuccess"] = true;
                return Page();
            }
            else
            {
                Message = "خطایی رخ داده است";
                return Page();
            }
        }
    }
}
