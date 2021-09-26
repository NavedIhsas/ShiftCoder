using System.Collections.Generic;
using _0_FrameWork.Application;
using _0_FrameWork.Domain.Infrastructure;

namespace CommentManagement.Infrastructure.Permissions
{
    // ReSharper disable  StringLiteralTypo
    public class CommentPermissionExposer:IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>()
            {
                {
                    "Comments",new List<PermissionDto>()
                    {
                        new PermissionDto(Permission.ListComments,"لیست نظرات"),
                        new PermissionDto(Permission.ApproveComments,"تایید نظرات"),
                        new PermissionDto(Permission.CancelComments,"کنسل نظرات"),
                        new PermissionDto(Permission.SearchComments,"سرچ در نظرات"),
                    }
                },

                 {
                    "LatestNews",new List<PermissionDto>()
                    {
                        new PermissionDto(Permission.CreateLatestNews,"ایجاد آخرین خبر"),
                        new PermissionDto(Permission.EditLatestNews,"ویرایش آخرین خبر"),
                     
                    }
                },

                 {
                    "HomePhoto",new List<PermissionDto>()
                    {
                        new PermissionDto(Permission.ChangePhotoHomePage,"تغییر عکس صفحه اصلی"),
                        new PermissionDto(Permission.CreatePhotoHomePage,"ایجاد عکس صفحه اصلی"),
                  
                    }
                }
            };
        }
    }
}
