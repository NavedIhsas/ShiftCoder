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
        private readonly IArticleCategoryQuery _category;
        private readonly ITeacherRepository _blogger;
        public ArticlesModel(IArticleQuery article, IArticleCategoryQuery category, ITeacherRepository blogger)
        {
            _article = article;
            _category = category;
            _blogger = blogger;
        }

        public SearchArticleQueryModel Search;
        public PaginationArticlesViewModel List;
        public List<ArticleCategoryQueryModel> Category;
        public List<Teacher> Blogger;
        public void OnGet(SearchArticleQueryModel search, List<long> bloggerId, List<string> categories,int pageId = 1)
        {
            List = _article.GetAllArticles(search,bloggerId,categories,pageId);
            Category = _category.GetAll();
            Blogger = _blogger.GetAllBlogger();
        }
    }
}
