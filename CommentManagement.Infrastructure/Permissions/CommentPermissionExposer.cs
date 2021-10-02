using System.Collections.Generic;
using _0_FrameWork.Application;
using _0_FrameWork.Domain.Infrastructure;

namespace CommentManagement.Infrastructure.Permissions
{
    // ReSharper disable  StringLiteralTypo
    public class CommentPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "Comments", new List<PermissionDto>
                    {
                        new(Permission.ListComments, "لیست نظرات"),
                        new(Permission.ApproveComments, "تایید نظرات"),
                        new(Permission.CancelComments, "کنسل نظرات"),
                        new(Permission.SearchComments, "سرچ در نظرات")
                    }
                },

                {
                    "Slider", new List<PermissionDto>
                    {
                        new(Permission.ChangePhotoHomePage, "تغییر عکس صفحه اصلی"),
                        new(Permission.CreatePhotoHomePage, "ایجاد عکس صفحه اصلی")
                    }
                }
            };
        }
    }
}