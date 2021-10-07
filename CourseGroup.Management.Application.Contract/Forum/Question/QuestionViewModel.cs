using System;

namespace Shop.Management.Application.Contract.Forum.Question
{
   public class QuestionViewModel
    {
        public long CourseId { get; set; }
        public long AccountId { get; set; }
        public string Body { get; set; }
        public string Title { get; set; }
        public DateTime ModifiedDate { get; set; }
    }

   public class CreateQuestionViewModel
   {
       public long CourseId { get; set; }
       public long AccountId { get; set; }
       public string Body { get; set; }
       public string Title { get; set; }
       public DateTime ModifiedDate { get; set; }
   }

   public class EditQuestionViewModel:CreateQuestionViewModel
    {
        public long Id { get; set; }
    }
}
