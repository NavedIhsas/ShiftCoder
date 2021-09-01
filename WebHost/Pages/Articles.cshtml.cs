using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShiftCoderQuery.Contract.Article;
using ShiftCoderQuery.Query;

namespace WebHost.Pages
{
    public class ArticlesModel : PageModel
    {
        private readonly IArticleQuery _article;

        public ArticlesModel(IArticleQuery article)
        {
            _article = article;
        }

        public List<GetAllArticleQueryModel> List;
        public void OnGet()
        {
            List = _article.GetAllArticles();
        }
    }
}
