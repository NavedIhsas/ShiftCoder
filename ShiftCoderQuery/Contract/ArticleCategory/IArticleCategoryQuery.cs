using System.Collections.Generic;

namespace ShiftCoderQuery.Contract.ArticleCategory
{
    public interface IArticleCategoryQuery
    {
        List<ArticleCategoryQueryModel> GetAll();
    }
}