using AccountManagement.Domain.Account.Agg;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShiftCoderQuery.Contract.Forum.Question;
using ShopManagement.Domain.ForumAgg;

namespace WebHost.Pages
{
    public class ForumModel : PageModel
    {
        private readonly IQuestionQuery _question;
        private readonly IAccountRepository _account;
        public ForumModel(IQuestionQuery question, IAccountRepository account)
        {
            _question = question;
            _account = account;
        }

        public void OnGet()
        {

        }
        public IActionResult OnGetQuestion(long id)
        {
            var question = new AddQuestionQueryModel()
            {
                CourseId = id
            };
            return Partial("Question", question);
        }

    public IActionResult OnPostQuestion(AddQuestionQueryModel question)
    {
        var questionId = _question.AddQuestion(question);
        return null;
    }
}
}
