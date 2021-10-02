using System.Collections.Generic;
using _0_FrameWork.Application;
using _0_FrameWork.Domain.Infrastructure;

namespace DiscountManagement.Infrastructure.Permissions
{
    public class DiscountPermissionExposer : IPermissionExposer
    {
        // ReSharper disable  StringLiteralTypo
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "CustomerDiscount", new List<PermissionDto>
                    {
                        new(Permission.ListCostumerDiscount, "مشاهده تخفیفات مشتری"),
                        new(Permission.SearchCostumerDiscount, "جستجوی تخفیفات مشتری"),
                        new(Permission.CreateCostumerDiscount, "ایجاد تخفیف مشتری"),
                        new(Permission.EditCostumerDiscount, " ویرایش تخفیف مشتری")
                    }
                },

                {
                    "ColleagueDiscount", new List<PermissionDto>
                    {
                        new(Permission.ListColleagueDiscount, "مشاهده تخفیفات همکار"),
                        new(Permission.SearchColleagueDiscount, "جستجو"),
                        new(Permission.CreateColleagueDiscount, "ایجاد تخفیف همکار"),
                        new(Permission.EditColleagueDiscount, "ویرایش تخفیف همکار"),
                        new(Permission.DeleteColleagueDiscount, "حذف تخفیف همکار"),
                        new(Permission.RestoreColleagueDiscount, "لغو حذف تخفیف همکار")
                    }
                },
                {
                    "DiscountCode", new List<PermissionDto>
                    {
                        new(Permission.ListDiscountCode, "مشاهده تخفیفات اختصاصی"),
                        new(Permission.SearchDiscountCode, "جستجو در کد های تخفیف"),
                        new(Permission.CreateDiscountCode, "ایجاد کد تخفیف"),
                        new(Permission.EditDiscountCode, "ویرایش کد تخفیف")
                    }
                }
            };
        }
    }
}