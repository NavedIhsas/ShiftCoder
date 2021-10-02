using System.Collections.Generic;
using _0_FrameWork.Application;
using _0_FrameWork.Domain.Infrastructure;

namespace WebHost.Permissions
{
    public class WebHostPermissionExposer : IPermissionExposer
    {
        // ReSharper disable StringLiteralTypo
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "AdminPanel", new List<PermissionDto>
                    {
                        new(Permission.AdministrationHomepage, "صفحه اصلی پنل ادمین"),
                        new(Permission.AdministrationNotifications, "آخرین اعلانات امروز")
                    }
                },
                {
                    "SiteActivity", new List<PermissionDto>
                    {
                        new(Permission.SystemAdministratorActivity, "فعالیت مدیران سیستم"),
                        new(Permission.SystemAdministratorNotification, "همه اعلانات"),
                        new(Permission.SystemAdministratorOrders, "مدیریت سفارشات")
                    }
                }
            };
        }
    }
}