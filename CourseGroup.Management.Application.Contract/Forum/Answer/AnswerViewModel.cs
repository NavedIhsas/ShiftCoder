namespace Shop.Management.Application.Contract.Forum.Answer
{
   public class AnswerViewModel
    {
        public long QuestionId { get; set; }
        public long AccountId { get; set; }
        public string Body { get; set; }
    }

   public class CreateAnswerViewModel
   {
       public long QuestionId { get; set; }
       public long AccountId { get; set; }
       public string Body { get; set; }
   }

   public class EditAnswerViewModel:CreateAnswerViewModel
   {
       public long Id { get; set; }
   }
}
