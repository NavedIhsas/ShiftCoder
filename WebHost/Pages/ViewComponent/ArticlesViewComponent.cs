using Microsoft.AspNetCore.Mvc;
using ShiftCoderQuery.Contract.Article;

namespace WebHost.Pages.ViewComponent
{
    public class ArticlesViewComponent:Microsoft.AspNetCore.Mvc.ViewComponent
    {
        private readonly IArticleQuery _article;

        public ArticlesViewComponent(IArticleQuery article)
        {
            _article = article;
        }

        public IViewComponentResult Invoke()
        {
            var article = _article.LatestArticle();
            return View("Default", article);
        }
    }
}
