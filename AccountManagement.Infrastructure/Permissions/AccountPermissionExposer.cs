using System.Collections.Generic;
using _0_FrameWork.Application;
using _0_FrameWork.Domain.Infrastructure;

namespace AccountManagement.Infrastructure.Permissions
{
   public class AccountPermissionExposer:IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            // ReSharper disable  StringLiteralTypo
            return new Dictionary<string, List<PermissionDto>>()
            {
                {
                    "Users", new List<PermissionDto>()
                    {
                       
                        new PermissionDto(Permission.ListUsers,"لیست کاربران"),
                        new PermissionDto(Permission.SearchUsers,"جستجو"),
                        new PermissionDto(Permission.EditUsers,"ویرایش کاربر"),
                        new PermissionDto(Permission.CreateUsers,"ایجاد کاربر"),
                        new PermissionDto(Permission.BlockUsers,"مسدود کردن"),
                        new PermissionDto(Permission.UnBlockUsers,"رفع انسداد کاربر"),
                        new PermissionDto(Permission.ChangePasswordUsers,"تغییر رمز کاربر"),
                        new PermissionDto(Permission.ListBlockedUsers,"مشاهده کاربران مسدود شده"),
                    }
                },
                {
                   "Teachers",new List<PermissionDto>()
                   {
                       new PermissionDto(Permission.ListTeacherAndBlogger,"مشاهده لیست"),
                       new PermissionDto(Permission.EditTeacherAndBlogger,"ویرایش"),
                       new PermissionDto(Permission.DeleteTeacherAndBlogger,"حذف"),
                   }
                }
               
            };
        }
    }
}
