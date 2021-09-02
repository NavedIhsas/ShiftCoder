using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShiftCoderQuery.Contract.Article;
using ShiftCoderQuery.Contract.ArticleCategory;

namespace WebHost.Pages
{
    public class ArticlesModel : PageModel
    {
        private readonly IArticleQuery _article;
        private readonly IArticleCategoryQuery _category;
        public ArticlesModel(IArticleQuery article, IArticleCategoryQuery category)
        {
            _article = article;
            _category = category;
        }

        public SearchArticleQueryModel Search;
        public List<GetAllArticleQueryModel> List;
        public List<ArticleCategoryQueryModel> Category;
        public void OnGet(SearchArticleQueryModel search)
        {
            List = _article.GetAllArticles(search);
            Category = _category.GetAll();
        }
    }
}
