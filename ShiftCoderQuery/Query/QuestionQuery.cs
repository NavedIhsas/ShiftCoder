using AccountManagement.Domain.Account.Agg;
using ShiftCoderQuery.Contract.Forum.Question;
using ShopManagement.Domain.ForumAgg;
using ShopManagement.Infrastructure.EfCore;

namespace ShiftCoderQuery.Query
{
    public class QuestionQuery:IQuestionQuery
    {
        private readonly ShopContext _context;
        private readonly IAccountRepository _account;

        public QuestionQuery(ShopContext context, IAccountRepository account)
        {
            _context = context;
            _account = account;
        }
        public long AddQuestion(AddQuestionQueryModel command)
        {
            var question = new Question(command.CourseId, command.AccountId, command.Body, command.Title);
            _context.Questions.Add(question);
            _context.SaveChanges();
            return question.Id;


        }
    }
}
