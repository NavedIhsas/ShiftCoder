using System.Collections.Generic;

namespace ShiftCoderQuery.Contract.Article
{
    public interface IArticleQuery
    {
        List<LatestArticleQueryModel> LatestArticle();
        List<GetAllArticleQueryModel> GetAllArticles(SearchArticleQueryModel search);
        SinglePageArticleQueryModel GetSingleArticleBy(string slug,string ipAddress);
    }
}