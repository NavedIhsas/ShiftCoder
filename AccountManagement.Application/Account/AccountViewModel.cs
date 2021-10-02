namespace AccountManagement.Application.Contract.Account
{
    public class AccountViewModel
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Eml { get; set; }
        public bool EmailConfirm { get; set; }
        public string CreationDate { get; set; }
        public string RoleName { get; set; }
        public long RoleId { get; set; }
    }
}