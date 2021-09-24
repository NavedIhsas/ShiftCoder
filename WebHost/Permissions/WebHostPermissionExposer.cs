using System.Collections.Generic;
using _0_FrameWork.Application;
using _0_FrameWork.Domain.Infrastructure;

namespace WebHost.Permissions
{
    public class WebHostPermissionExposer:IPermissionExposer
    {
        // ReSharper disable StringLiteralTypo
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>()
            {
                {
                    "AdminPanel",new List<PermissionDto>()
                    {
                        new PermissionDto(Permission.AdministrationHomepage,"صفحه اصلی پنل ادمین"),
                        new PermissionDto(Permission.AdministrationNotifications,"آخرین اعلانات امروز"),
                    }
                },
                {
                    "SiteActivity",new List<PermissionDto>()
                    {
                        new PermissionDto(Permission.SystemAdministratorActivity,"فعالیت مدیران سیستم"),
                        new PermissionDto(Permission.SystemAdministratorNotification,"همه اعلانات"),
                        new PermissionDto(Permission.SystemAdministratorOrders,"مدیریت سفارشات"),
                    }
                }
            };
        }
    }
}
