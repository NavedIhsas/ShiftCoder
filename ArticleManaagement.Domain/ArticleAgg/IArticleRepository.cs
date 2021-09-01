using System;
using System.Collections.Generic;
using _0_FrameWork.Domain;
using BlogManagement.Application.Contract.Article;

namespace BlogManagement.Domain.ArticleAgg
{
    public interface IArticleRepository:IRepository<long,Article>
    {
        EditArticleViewModel GetDetails(long id);
        List<ArticleViewModel> Search(ArticleSearchModel search);
        bool? GetPublishStatus(long articleId);
        DateTime? GetPublishDate(long articleId);

    }
}