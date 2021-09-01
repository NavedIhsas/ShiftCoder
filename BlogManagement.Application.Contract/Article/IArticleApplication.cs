using System.Collections.Generic;
using _0_FrameWork.Application;

namespace BlogManagement.Application.Contract.Article
{
    public interface IArticleApplication
    {
        OperationResult Create(CreateArticleViewModel command);
        OperationResult Edit(EditArticleViewModel command);
        EditArticleViewModel GetDetails(long id);
        List<ArticleViewModel> Search(ArticleSearchModel search);
    }
}
