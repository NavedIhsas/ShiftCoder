using System.Collections.Generic;
using _0_FrameWork.Domain;
using BlogManagement.Application.Contract.ArticleCategory;

namespace BlogManagement.Domain.ArticleCategoryAgg
{
    public interface IArticleCategoryRepository:IRepository<long,ArticleCategory>
    {
        EditArticleCategoryViewModel GetDetails(long id);
        List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel search);
        List<ArticleCategoryViewModel> SelectList();
        string GetArticleCategoryName(long articleCategoryId);
    }
}