using _0_FrameWork.Application;
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

        public SingleArticleModel(IArticleQuery article, ICommentApplication comment, IAuthHelper authHelper, ICommentRepository commentRepository)
        {
            _article = article;
            _comment = comment;
            _authHelper = authHelper;
            _commentRepository = commentRepository;
        }

        public SinglePageArticleQueryModel Article;
        public void OnGet(string id)
        {
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            Article = _article.GetSingleArticleBy(id,ipAddress);
        }
        public IActionResult OnPost(CreateCommentViewModel command, string articleSlug)
        {
            if (_authHelper.IsAuthenticated())
            {
                command.Type = ThisType.Article;
                var createComment = new Comment(_authHelper.CurrentAccountFullName(), User.Identity.Name, command.Message
                    , command.OwnerRecordId, command.Type, command.ParentId);
                _commentRepository.Create(createComment);
                _commentRepository.SaveChanges();
                return RedirectToPage(new { id = articleSlug });
            }

            command.Type = ThisType.Article;
            var post = _comment.Create(command);
            ViewData["IsSuccess"] = true;
            return RedirectToPage(new{id=articleSlug});
        }

    }
}
