using System.Collections.Generic;
using _0_FrameWork.Application;
using _0_FrameWork.Domain.Infrastructure;

namespace BlogManagement.Infrastructure.Permissions
{
    public class BlogPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            // ReSharper disable  StringLiteralTypo
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "Articles", new List<PermissionDto>
                    {
                        new(Permission.ListArticles, "لیست مقاله ها"),
                        new(Permission.SearchArticles, "جستجو"),
                        new(Permission.CreateArticles, "ایجاد مقاله"),
                        new(Permission.EditListArticles, "ویرایش مقاله")
                    }
                },
                {
                    "ArticleCategories", new List<PermissionDto>
                    {
                        new(Permission.ListArticleCategories, "لیست گرو ها"),
                        new(Permission.SearchArticleCategories, "جستجو"),
                        new(Permission.CreateArticleCategories, "ایجاد گروه"),
                        new(Permission.EditArticleCategories, "ویرایش گروه")
                    }
                }
            };
        }
    }
}