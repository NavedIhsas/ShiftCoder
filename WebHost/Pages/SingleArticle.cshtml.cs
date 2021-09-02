using Microsoft.AspNetCore.Mvc.RazorPages;
using ShiftCoderQuery.Contract.Article;

namespace WebHost.Pages
{
    public class SingleArticleModel : PageModel
    {
        private readonly IArticleQuery _article;

        public SingleArticleModel(IArticleQuery article)
        {
            _article = article;
        }

        public SinglePageArticleQueryModel Article;
        public void OnGet(string id)
        {
            Article = _article.GetSingleArticleBy(id);
        }
    }
}
