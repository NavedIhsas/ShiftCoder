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
            return new Dictionary<string, List<PermissionDto>>()
            {
                {
                    "CustomerDiscount", new List<PermissionDto>()
                    {
                      
                        new PermissionDto(Permission.ListCostumerDiscount, "مشاهده تخفیفات مشتری"),
                        new PermissionDto(Permission.SearchCostumerDiscount, "جستجوی تخفیفات مشتری"),
                        new PermissionDto(Permission.CreateCostumerDiscount, "ایجاد تخفیف مشتری"),
                        new PermissionDto(Permission.EditCostumerDiscount, " ویرایش تخفیف مشتری")
                    }
                },

                {
                    "ColleagueDiscount",new List<PermissionDto>()
                       {
                            new PermissionDto(Permission.ListColleagueDiscount,"مشاهده تخفیفات همکار"),
                            new PermissionDto(Permission.SearchColleagueDiscount,"جستجو"),
                            new PermissionDto(Permission.CreateColleagueDiscount,"ایجاد تخفیف همکار"),
                            new PermissionDto(Permission.EditColleagueDiscount,"ویرایش تخفیف همکار"),
                            new PermissionDto(Permission.DeleteColleagueDiscount,"حذف تخفیف همکار"),
                            new PermissionDto(Permission.RestoreColleagueDiscount,"لغو حذف تخفیف همکار"),
                       }
                },
                {
                    "DiscountCode",new List<PermissionDto>()
                    {
                        new PermissionDto(Permission.ListDiscountCode,"مشاهده تخفیفات اختصاصی"),
                        new PermissionDto(Permission.SearchDiscountCode,"جستجو در کد های تخفیف"),
                        new PermissionDto(Permission.CreateDiscountCode,"ایجاد کد تخفیف"),
                        new PermissionDto(Permission.EditDiscountCode,"ویرایش کد تخفیف"),
                    }
                }
            };
        }
    }
}
