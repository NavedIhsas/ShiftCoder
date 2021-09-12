namespace AccountManagement.Domain.Account.Agg
{
    public class Teacher
    {
        public long Id { get;private set; }
        public string Skills { get; private set; }
        public string Bio { get; private set; }
        public string Resumes { get; private set; }
        public long AccountId { get; private set; }
        public Account Account { get; private set; }

        public Teacher(string skills, string bio, string resumes, long accountId)
        {
            Skills = skills;
            Bio = bio;
            Resumes = resumes;
            AccountId = accountId;
        }

        public void Edit(string skills, string bio, string resumes, long accountId)
        {
            Skills = skills;
            Bio = bio;
            Resumes = resumes;
            AccountId = accountId;
        }
    }

}
