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
               },
               {
                   "CourseEpisode", new List<PermissionDto>()
                   {
                       new PermissionDto(_0_FrameWork.Application.Permission.ListCourseEpisodes,"لیست فایل های دوره"),
                       new PermissionDto(_0_FrameWork.Application.Permission.CreateCourseEpisodes,"ایجاد فایل برای دوره"),
                       new PermissionDto(_0_FrameWork.Application.Permission.EditCourseEpisodes,"ویرایش فایل های دوره ها"),
                     
                   }

               },

               {
                   "CourseStatus", new List<PermissionDto>()
                   {
                       new PermissionDto(_0_FrameWork.Application.Permission.CreateCourseStatus,"ایجاد وضعیت دوره ها"),
                       new PermissionDto(_0_FrameWork.Application.Permission.EditCourseStatus,"ویرایش وضعیت دوره ها"),
                   }

               },
               {
                   "CourseLevel", new List<PermissionDto>()
                   {
                       new PermissionDto(_0_FrameWork.Application.Permission.CreateCourseLevel,"ایجاد سطح دور ها"),
                       new PermissionDto(_0_FrameWork.Application.Permission.EditCourseLevel,"ویرایش سطح دوره ها"),
                   }

               },

           };
        }
    }
}
