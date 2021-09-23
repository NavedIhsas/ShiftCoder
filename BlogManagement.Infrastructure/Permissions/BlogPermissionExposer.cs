using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_FrameWork.Domain.Infrastructure;
using AccountManagement.Domain.RoleAgg;
using Permission = _0_FrameWork.Application.Permission;

namespace BlogManagement.Infrastructure.Permissions
{
   public class BlogPermissionExposer:IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        { // ReSharper disable  StringLiteralTypo
            return new Dictionary<string, List<PermissionDto>>()
            {
                {
                    "Articles", new List<PermissionDto>()
                    {
                        new PermissionDto(Permission.ListArticles,"لیست مقاله ها"),
                        new PermissionDto(Permission.SearchArticles,"جستجو"),
                        new PermissionDto(Permission.CreateArticles,"ایجاد مقاله"),
                        new PermissionDto(Permission.EditListArticles,"ویرایش مقاله"),
                    }
                },
                {
                    "ArticleCategories", new List<PermissionDto>()
                    {
                        new PermissionDto(Permission.ListArticleCategories,"لیست گرو ها"),
                        new PermissionDto(Permission.SearchArticleCategories,"جستجو"),
                        new PermissionDto(Permission.CreateArticleCategories,"ایجاد گروه"),
                        new PermissionDto(Permission.EditArticleCategories,"ویرایش گروه"),
                    }
                }

            };
        }
    }
}
