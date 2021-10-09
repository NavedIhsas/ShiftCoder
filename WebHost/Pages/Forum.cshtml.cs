using System.Collections.Generic;
using AccountManagement.Domain.Account.Agg;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShiftCoderQuery.Contract.Forum.Answer;
using ShiftCoderQuery.Contract.Forum.Question;

namespace WebHost.Pages
{
    public class ForumModel : PageModel
    {
        public QuestionPagination Pagination;
        public QuestionQueryModel Question;
        private readonly IForumQuery _question;
        private readonly IAccountRepository _account;
        public ForumModel(IForumQuery question, IAccountRepository account)
        {
            _question = question;
            _account = account;
        }

        public void OnGet(long id,int pageId=1)
        {
            Pagination = _question.QuestionCourse(id,pageId);
            ViewData["CourseId"] = id;
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
            return Redirect($"/Forum/{questionId}?handler=ShowQuestion");
        }

        public IActionResult OnGetShowQuestion(long id,int pageId=1)
        {
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            Question = _question.ShowQuestion(id,ipAddress,pageId);
            return Partial("ShowQuestion",Question);
        }

        public IActionResult OnPostShowAnswer(AddAnswerQueryModel command)
        {
             _question.AddAnswer(command);
             return Redirect($"/Forum/{command.QuestionId}?handler=ShowQuestion");
        }
    }
}
