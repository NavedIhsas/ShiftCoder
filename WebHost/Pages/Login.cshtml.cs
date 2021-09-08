using AccountManagement.Application.Contract.Account;
using AccountManagement.Domain.Account.Agg;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebHost.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IAccountRepository _account;
        public LoginModel(IAccountRepository account)
        {
            _account = account;
        }

        [TempData] public string Message { get; set; }
        public LoginViewModel Login;
        public void OnGet()
        {
        }

        public IActionResult OnPost(LoginViewModel login)
        {
            if (!ModelState.IsValid) return Page();
            var userLogin = _account.Login(login);
            if (userLogin.IsSucceeded)
            {
                ViewData["IsSuccess"] = true;
                return Page();
            }

            Message = userLogin.Message;
            return Page();
        }

        public IActionResult OnGetLogout()
        {
           _account.Logout();
           return RedirectToPage("Login");
        }
    }
}
