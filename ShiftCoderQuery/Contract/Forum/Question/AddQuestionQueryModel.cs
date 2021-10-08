namespace ShiftCoderQuery.Contract.Forum.Question
{
    public class AddQuestionQueryModel
    {
        public long CourseId { get; set; }
        public long? AccountId { get; set; }
        public string Body { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public bool IsTrue { get; set; }
    }
}