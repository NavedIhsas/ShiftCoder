using System.Collections.Generic;
using _0_FrameWork.Application;
using _0_FrameWork.Domain.Infrastructure;
using AccountManagement.Application.Contract.Account;
using AccountManagement.Domain.Account.Agg;
using BlogManagement.Application.Contract.Article;
using BlogManagement.Application.Contract.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebHost.Areas.Administration.Pages.Blog.Article
{
    public class IndexModel : PageModel
    {
        public SelectList SelectList;
        public ArticleSearchModel SearchModel;
        public List<BlogManagement.Application.Contract.Article.ArticleViewModel> Article;

        private readonly IArticleApplication _article;
        private readonly IArticleCategoryApplication _categoryApplication;
        private readonly ITeacherRepository _teacher;
        public IndexModel(IArticleApplication article, IArticleCategoryApplication categoryApplication, ITeacherRepository teacher)
        {
            this._article = article;
            this._categoryApplication = categoryApplication;
            _teacher = teacher;
        }

        [NeedPermission(Permission.ListArticles)]
        public void OnGet(ArticleSearchModel searchModel)
        {
            SelectList = new SelectList(_categoryApplication.SelectList(), "Id", "Name");
            Article = _article.Search(searchModel);
        }

        [NeedPermission(Permission.CreateArticles)]
        public IActionResult OnGetCreate()
        {
            var course = new CreateArticleViewModel()
            {
                SelectList = _categoryApplication.SelectList(),
               BloggerSelectList = _teacher.SelectListForArticles(),
            };
            return Partial("./Create", course);
        }

        public IActionResult OnPostCreate(CreateArticleViewModel command)
        {
            var course = _article.Create(command);
            return RedirectToPage("Index");
        }

        [NeedPermission(Permission.EditListArticles)]
        public IActionResult OnGetEdit(long id)
        {
            var course = _article.GetDetails(id);

            course.SelectList = _categoryApplication.SelectList();
            course.BloggerSelectList = _teacher.SelectListForArticles();
            return Partial("./Edit", course);

        }

        public IActionResult OnPostEdit(EditArticleViewModel command)
        {
            var course = _article.Edit(command);
            return RedirectToPage("Index");
        }
    }
}
