using System.Collections.Generic;
using AccountManagement.Domain.Account.Agg;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShiftCoderQuery.Contract.Article;
using ShiftCoderQuery.Contract.ArticleCategory;

namespace WebHost.Pages
{
    public class ArticlesModel : PageModel
    {
        private readonly IArticleQuery _article;
        private readonly ITeacherRepository _blogger;
        private readonly IArticleCategoryQuery _category;
        public List<Teacher> Blogger;
        public List<ArticleCategoryQueryModel> Category;
        public PaginationArticlesViewModel List;

        public SearchArticleQueryModel Search;

        public ArticlesModel(IArticleQuery article, IArticleCategoryQuery category, ITeacherRepository blogger)
        {
            _article = article;
            _category = category;
            _blogger = blogger;
        }

        public void OnGet(SearchArticleQueryModel search, List<long> bloggerId, List<string> categories, int pageId = 1)
        {
            List = _article.GetAllArticles(search, bloggerId, categories, pageId);
            Category = _category.GetAll();
            Blogger = _blogger.GetAllBlogger();
        }
    }
}