using System.Collections.Generic;
using _0_FrameWork.Application;

namespace BlogManagement.Application.Contract.ArticleCategory
{
    public interface IArticleCategoryApplication
    {
        OperationResult Create(CreateArticleCategoryViewModel command);
        OperationResult Edit(EditArticleCategoryViewModel command);
        EditArticleCategoryViewModel GetDetails(long id);
        List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel search);
        List<ArticleCategoryViewModel> SelectList();

    }
}
