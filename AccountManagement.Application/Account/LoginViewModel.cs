namespace AccountManagement.Application.Contract.Account
{
    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string Message { get; set; }
    }
}