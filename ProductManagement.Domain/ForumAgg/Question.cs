using System;
using System.Collections.Generic;
using _0_FrameWork.Domain;

namespace ShopManagement.Domain.ForumAgg
{
   public class Question:EntityBase
    {
        public Question(long courseId, long? accountId, string body, string title)
        {
            CourseId = courseId;
            AccountId = accountId;
            Body = body;
            Name = title;
            ModifiedDate = DateTime.Now;
        }

        public Question()
        {
            
        }
        public long CourseId { get;private set; }
        public long? AccountId { get;private set; }
        public string Body { get; private set; }
        public string Name { get; private set; }
        public DateTime ModifiedDate { get; private set; }
        public List<Answer> Answers { get; set; }
    }

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

    public interface IAnswerRepository:IRepository<long, Answer>
    {

    }

    public interface IQuestionsRepository:IRepository<long,Question>
   {

    }
}
