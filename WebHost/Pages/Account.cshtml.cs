using System.Threading.Tasks;
using _0_FrameWork.Application;
using AccountManagement.Application.Contract.Account;
using AccountManagement.Domain.Account.Agg;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebHost.Pages
{
    public class AccountModel : PageModel
    {
        [ViewData] public string Message { get; set; }
        [ViewData] public string Success { get; set; }

        [BindProperty]
        public RegisterUserViewModel Register { get; set; }
        private readonly IAccountApplication _account;
        private readonly IAccountRepository _accountRepository;
        private readonly IRazorPartialToStringRenderer _renderView;
       
        public AccountModel(IAccountApplication account, IAccountRepository accountRepository, IRazorPartialToStringRenderer renderView)
        {
            _account = account;
            _accountRepository = accountRepository;
            _renderView = renderView;
        }

       //Register
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            var register = await _account.Create(Register);
            if (!register.IsSucceeded)
            {
                ViewData["ExistEmail"] = true;
                return Page();
            }
            ViewData["IsSuccess"] = true;
            return Page();

        }

        //Logout
        public IActionResult OnGetLogout()
        {
            _account.Logout();
            return RedirectToPage("/Index");
        }


        //Confirm email
        public IActionResult OnGetConfirmEmail(string id)
        {
            var user = _accountRepository.EmailConfirm(id);
            if (user == false) return NotFound();
            return Partial("SuccessEmailConfirmed");
        }


        //forgot password
        public IActionResult OnGetForgotPassword()
        {
            return Partial("ForgotPassword", new ForgotPasswordViewModel());
        }

       
        public async Task<IActionResult> OnPostForgotPassword(ForgotPasswordViewModel command)
        {
            var user = await _account.ForgotPassword(command);
            if (user == false)
            {
                Message = ApplicationMessage.CheckEmailForExist;
                return this.OnGetForgotPassword();
            }

            Success = ApplicationMessage.ResetPasswordEmailSent;
           return this.OnGetForgotPassword();
        }


        //reset password
        public IActionResult OnGetResetPassword(string id)
        {
            var user = _accountRepository.GetUserByActiveCode(id);
            if (user == null) return NotFound();
            return Partial("ResetPassword",new ResetPasswordViewModel(){ActiveCode  =id});
        }
        public IActionResult OnPostResetPassword(ResetPasswordViewModel command)
        {
            var user = _account.ResetPassword(command);
            if (user == false) return NotFound();
            Success = $"رمز عبور شما با موفقیت تغییر کرد لطفا وارد سایت شوید";
            return Redirect("/Login");
        }
    }
}
