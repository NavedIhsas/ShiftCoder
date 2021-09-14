using _0_FrameWork.Application;
using CommentManagement.Application.Contract.Comment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShiftCoderQuery.Contract.Article;

namespace WebHost.Pages
{
    public class SingleArticleModel : PageModel
    {
        private readonly IArticleQuery _article;
        private readonly ICommentApplication _comment;

        public SingleArticleModel(IArticleQuery article, ICommentApplication comment)
        {
            _article = article;
            _comment = comment;
        }

        public SinglePageArticleQueryModel Article;
        public void OnGet(string id)
        {
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            Article = _article.GetSingleArticleBy(id,ipAddress);
        }
        public IActionResult OnPost(CreateCommentViewModel command, string articleSlug)
        {
            command.Type = OwnerType.Article;
            var post = _comment.Create(command);
            ViewData["IsSuccess"] = true;
            return RedirectToPage(new{id=articleSlug});
        }

    }
}
