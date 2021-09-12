using AccountManagement.Application.Contract.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebHost.Pages
{
    public class AccountModel : PageModel
    {
        private readonly IAccountApplication _account;

        public AccountModel(IAccountApplication account)
        {
            _account = account;
        }
        public void OnGetRegister()
        {

        }
        public IActionResult OnPostRegister(RegisterUserViewModel command)
        {
            if (!ModelState.IsValid) return Page();

            var register = _account.Create(command);
            if (register.IsSucceeded)
            {
                ViewData["IsSuccess"] = true;
                return Page();
            }

            return RedirectToPage("Index");
        }

        public IActionResult OnGetLogin()
        {
            return Partial("Login", new LoginViewModel());
        }
        public IActionResult OnPostLogin(LoginViewModel login)
        {
            var userLogin = _account.Login(login);
            return RedirectToPage("Index");
        }

        public void OnGetLogout()
        {
            _account.Logout();
        }
    }
}
