using System.Threading.Tasks;
using AccountManagement.Application.Contract.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebHost.Pages
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly IAccountApplication _account;

        public ForgotPasswordModel(IAccountApplication account)
        {
            _account = account;
        }


        //forgot password
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(ForgotPasswordViewModel command)
        {
            var user = await _account.ForgotPassword(command);
            if (user == false)
            {
                ViewData["Message"] = true;
                return Page();
            }

            ViewData["Success"] = true;
            return Page();
        }
    }
}