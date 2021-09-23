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
                }
            };
        }
    }
}
