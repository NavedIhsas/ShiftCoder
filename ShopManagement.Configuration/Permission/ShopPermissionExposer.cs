using System.Collections.Generic;
using _0_FrameWork.Domain.Infrastructure;

namespace ShopManagement.Configuration.Permission
{
    // ReSharper disable StringLiteralTypo
    public class ShopPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>()
           {
               {
                  
                   "Courses", new List<PermissionDto>()
                   {
                       new PermissionDto(_0_FrameWork.Application.Permission.ListCourses,"لیست دوره ها"),
                       new PermissionDto(_0_FrameWork.Application.Permission.SearchCourses,"سرچ دوره"),
                       new PermissionDto(_0_FrameWork.Application.Permission.CreateCourses,"ایجاد دوره"),
                       new PermissionDto(_0_FrameWork.Application.Permission.EditCourses,"ویرایش دوره"),
                   }
               },
               {
                   "CourseGroup", new List<PermissionDto>()
                   {
                       new PermissionDto(_0_FrameWork.Application.Permission.ListCourseGroups,"لیست گروه های دوره ها"),
                       new PermissionDto(_0_FrameWork.Application.Permission.SearchCourseGroups,"جستجوی گروه"),
                       new PermissionDto(_0_FrameWork.Application.Permission.CreateCourseGroups,"ایجاد گروه"),
                       new PermissionDto(_0_FrameWork.Application.Permission.EditCourseGroups,"ویرایش گروه"),
                       new PermissionDto(_0_FrameWork.Application.Permission.DeleteCourseGroups,"حذف گروه"),
                       new PermissionDto(_0_FrameWork.Application.Permission.RestoreCourseGroups,"لغو حذف گروه"),
                   }
               }

           };
        }
    }
}
