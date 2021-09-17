namespace AccountManagement.Domain.Account.Agg
{
    public class Teacher
    {
        public long Id { get;private set; }
        public string Skills { get; private set; }
        public string Bio { get; private set; }
        public string Resumes { get; private set; }
        public long AccountId { get; private set; }
        public int Type { get;private set; }
       
        public Account Account { get; private set; }

        public Teacher()
        {
            
        }
        public Teacher(string skills, string bio, string resumes, long accountId, int type)
        {
            Skills = skills;
            Bio = bio;
            Resumes = resumes;
            AccountId = accountId;
            Type = type;
        }

        public void Edit(string skills, string bio, string resumes, long accountId, int type)
        {
            Skills = skills;
            Bio = bio;
            Resumes = resumes;
            AccountId = accountId;
            Type = type;
        }
    }

}
