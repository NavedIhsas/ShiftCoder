using System.Collections.Generic;
using _0_FrameWork.Application;
using _0_FrameWork.Domain.Infrastructure;

namespace AccountManagement.Infrastructure.Permissions
{
    public class AccountPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            // ReSharper disable  StringLiteralTypo
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "Users", new List<PermissionDto>
                    {
                        new(Permission.ListUsers, "لیست کاربران"),
                        new(Permission.SearchUsers, "جستجو"),
                        new(Permission.EditUsers, "ویرایش کاربر"),
                        new(Permission.CreateUsers, "ایجاد کاربر"),
                        new(Permission.BlockUsers, "مسدود کردن"),
                        new(Permission.UnBlockUsers, "رفع انسداد کاربر"),
                        new(Permission.ChangePasswordUsers, "تغییر رمز کاربر"),
                        new(Permission.ListBlockedUsers, "مشاهده کاربران مسدود شده")
                    }
                },

                {
                    "Roles", new List<PermissionDto>
                    {
                        new(Permission.ListRoles, "لیست نقش ها"),
                        new(Permission.EditRoles, "ویرایش نقش ها"),
                        new(Permission.CreateRoles, "ایجاد نقش"),
                    }
                },
                {
                    "Teachers", new List<PermissionDto>
                    {
                        new(Permission.ListTeacherAndBlogger, "مشاهده لیست"),
                        new(Permission.EditTeacherAndBlogger, "ویرایش"),
                        new(Permission.DeleteTeacherAndBlogger, "حذف")
                    }
                }
            };
        }
    }
}