using System.Collections.Generic;
using _0_FrameWork.Application;
using AccountManagement.Domain.Account.Agg;
using CommentManagement.Application.Contract.Comment;
using CommentManagement.Domain.CourseCommentAgg;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShiftCoderQuery.Contract.Article;
using ShiftCoderQuery.Contract.ArticleCategory;

namespace WebHost.Pages
{
    public class SingleArticleModel : PageModel
    {
        private readonly IAccountRepository _account;
        private readonly IArticleQuery _article;
        private readonly IAuthHelper _authHelper;
        private readonly IArticleCategoryQuery _category;
        private readonly ICommentApplication _comment;
        private readonly ICommentRepository _commentRepository;
        public Account Account;

        public SinglePageArticleQueryModel Article;
        public List<ArticleCategoryQueryModel> Categories;

        public SingleArticleModel(IArticleQuery article, ICommentApplication comment, IAuthHelper authHelper,
            ICommentRepository commentRepository, IAccountRepository account, IArticleCategoryQuery category)
        {
            _article = article;
            _comment = comment;
            _authHelper = authHelper;
            _commentRepository = commentRepository;
            _account = account;
            _category = category;
        }

        public void OnGet(string id, string type = "")
        {
            ViewData["Type"] = type;
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            Article = _article.GetSingleArticleBy(id, ipAddress);
            Categories = _category.GetAll();
        }

        public IActionResult OnPost(CreateCommentViewModel command, string articleSlug)
        {
            const string type = "send-comment";
            if (_authHelper.IsAuthenticated())
            {
                Account = _account.GetUserBy(User.Identity.Name);

                command.Type = ThisType.Article;
                var createComment = new Comment(_authHelper.CurrentAccountFullName(), User.Identity.Name,
                    command.Message
                    , command.OwnerRecordId, command.Type, command.ParentId, Account.Avatar);

                _commentRepository.Create(createComment);
                _commentRepository.SaveChanges();
                return RedirectToPage(new { id = articleSlug, type });
            }

            command.Type = ThisType.Article;
            var post = _comment.Create(command);
            ViewData["IsSuccess"] = true;
            return RedirectToPage(new { id = articleSlug, type });
        }
    }
}