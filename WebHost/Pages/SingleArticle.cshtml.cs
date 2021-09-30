using _0_FrameWork.Application;
using AccountManagement.Domain.Account.Agg;
using CommentManagement.Application.Contract.Comment;
using CommentManagement.Domain.CourseCommentAgg;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShiftCoderQuery.Contract.Article;

namespace WebHost.Pages
{
    public class SingleArticleModel : PageModel
    {
        private readonly IArticleQuery _article;
        private readonly ICommentApplication _comment;
        private readonly IAuthHelper _authHelper;
        private readonly ICommentRepository _commentRepository;
        private readonly IAccountRepository _account;

        public SingleArticleModel(IArticleQuery article, ICommentApplication comment, IAuthHelper authHelper, ICommentRepository commentRepository, IAccountRepository account)
        {
            _article = article;
            _comment = comment;
            _authHelper = authHelper;
            _commentRepository = commentRepository;
            _account = account;
        }

        public SinglePageArticleQueryModel Article;
        public Account Account;
        public void OnGet(string id, string type = "")
        {
            ViewData["Type"] = type.ToString();
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            Article = _article.GetSingleArticleBy(id, ipAddress);
        }
        public IActionResult OnPost(CreateCommentViewModel command, string articleSlug)
        {
            const string type = "send-comment";
            if (_authHelper.IsAuthenticated())
            {
                Account = _account.GetUserBy(User.Identity.Name);

                command.Type = ThisType.Article;
                var createComment = new Comment(_authHelper.CurrentAccountFullName(), User.Identity.Name, command.Message
                    , command.OwnerRecordId, command.Type, command.ParentId, Account.Avatar);

                _commentRepository.Create(createComment);
                _commentRepository.SaveChanges();
                return RedirectToPage(new { id = articleSlug, type });
            }

            command.Type = ThisType.Article;
            var post = _comment.Create(command);
            ViewData["IsSuccess"] = true;
            return RedirectToPage(new { id = articleSlug,type});
        }

    }
}
