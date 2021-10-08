namespace ShiftCoderQuery.Contract.Forum.Answer
{
    public class AddAnswerQueryModel
    {
        public long QuestionId { get; set; }
        public long? AccountId { get; set; }
        public string Body { get; set; }
        public string Name { get; set; }
    }
}