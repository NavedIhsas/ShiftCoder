using System;

namespace ShiftCoderQuery.Contract.Forum.Question
{
    public class QuestionQueryModel
    {
        public long CourseId { get; set; }
        public long AccountId { get; set; }
        public string Body { get; set; }
        public string Title { get; set; }
        public DateTime ModifiedDate { get; set; }
    }

    public class AddQuestionQueryModel
    {
        public long CourseId { get; set; }
        public long? AccountId { get; set; }
        public string Body { get; set; }
        public string Title { get; set; }
        public DateTime ModifiedDate { get; set; }
    }

    public interface IQuestionQuery
    {
        long AddQuestion(AddQuestionQueryModel command);
    }
}
