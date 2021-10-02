using AccountManagement.Application.Contract.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebHost.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IAccountApplication _account;

        public LoginModel(IAccountApplication account)
        {
            _account = account;
        }


        public void OnGet()
        {
        }

        public IActionResult OnPost(LoginViewModel login, string ReturnUrl = "Index")
        {
            var user = _account.Login(login);
            if (user == false)
            {
                ViewData["Message"] = true;
                return Page();
            }

            ViewData["Success"] = true;
            if (ReturnUrl != "Index") Redirect(ReturnUrl);
            return Page();
        }
    }
}