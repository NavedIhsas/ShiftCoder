namespace _0_FrameWork.Application
{
    public class AuthHelperViewModel
    {
        public long AccountId { get; set; }
        public long RoleId { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Phone { get; internal set; }
    }
}