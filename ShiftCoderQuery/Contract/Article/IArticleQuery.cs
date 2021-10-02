using System.Collections.Generic;

namespace ShiftCoderQuery.Contract.Article
{
    public interface IArticleQuery
    {
        List<LatestArticleQueryModel> LatestArticle();

        PaginationArticlesViewModel GetAllArticles(SearchArticleQueryModel search, List<long> bloggerId,
            List<string> categories, int pageId = 1);

        SinglePageArticleQueryModel GetSingleArticleBy(string slug, string ipAddress);
    }
}