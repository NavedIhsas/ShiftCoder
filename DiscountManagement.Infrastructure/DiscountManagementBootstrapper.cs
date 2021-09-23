using DiscountManagement.Domain.CustomerDiscountAgg;
using Microsoft.Extensions.DependencyInjection;
using System;
using _0_FrameWork.Domain.Infrastructure;
using ColleagueDiscountManagementApplication.Contract.ColleagueDiscount;
using DiscountManagementInfrastructure.EfCore;
using DiscountManagementInfrastructure.EfCore.Repository;
using Microsoft.EntityFrameworkCore;
using DiscountManagement.Domain.ColleagueDiscountAgg;
using DiscountManagement.Application;
using ColleagueDiscountManagementApplication.Contract.CustomerDiscount;
using ColleagueDiscountManagementApplication.Contract.DiscountCode;
using DiscountManagement.Domain.DiscountCode;
using DiscountManagement.Domain.UserDiscountAgg;
using DiscountManagement.Infrastructure.Permissions;
using ShiftCoderQuery.Contract.Discount;
using ShiftCoderQuery.Query;

namespace DiscountManagement.Infrastructure
{
    public class DiscountManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<ICustomerDiscountApplication, CustomerApplication>();

            services.AddTransient<IColleagueRepository, ColleagueRepository>();
            services.AddTransient<IColleagueApplication, ColleagueApplication>();

            services.AddTransient<IDiscountCodeApplication, DiscountCodeApplication>();
            services.AddTransient<IDiscountCodeRepository, DiscountCodeRepository>();

            services.AddTransient<IDiscountQuery, DiscountQuery>();

            services.AddTransient<IUserDiscountRepository, UserDiscountRepository>();

            services.AddTransient<IPermissionExposer, DiscountPermissionExposer>();


            services.AddDbContext<DiscountContext>(option =>
            {
                option.UseSqlServer(connectionString);
            });
        }
    }
}
