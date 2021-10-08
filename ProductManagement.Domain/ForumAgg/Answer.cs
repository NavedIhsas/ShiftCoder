using _0_FrameWork.Domain;

namespace ShopManagement.Domain.ForumAgg
{
    public class Answer : EntityBase
    {
        public Answer(long questionId, long? accountId, string body, string name)
        {
            QuestionId = questionId;
            AccountId = accountId;
            Body = body;
            Name = name;
        }
        public long QuestionId { get;private set; }
        public long? AccountId { get; private set; }
        public string Name { get;private set; }
        public string Body { get; private set; }
        public Question Question { get; private set; }
    }
}