using System.Collections.Generic;
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
        public IndexModel(IArticleApplication article, IArticleCategoryApplication categoryApplication)
        {
            this._article = article;
            this._categoryApplication = categoryApplication;
        }

        public void OnGet(ArticleSearchModel searchModel)
        {
            SelectList = new SelectList(_categoryApplication.SelectList(), "Id", "Name");
            Article = _article.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var course = new CreateArticleViewModel()
            {
                SelectList = _categoryApplication.SelectList(),
               
            };
            return Partial("./Create", course);
        }

        public JsonResult OnPostCreate(CreateArticleViewModel command)
        {
            var course = _article.Create(command);
            return new JsonResult(course);
        }

        public IActionResult OnGetEdit(long id)
        {
            var course = _article.GetDetails(id);

            course.SelectList = _categoryApplication.SelectList();

            return Partial("./Edit", course);

        }

        public JsonResult OnPostEdit(EditArticleViewModel command)
        {
            var course = _article.Edit(command);
            return new JsonResult(course);
        }
    }
}
