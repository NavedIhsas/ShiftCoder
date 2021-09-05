using DiscountManagement.Domain.CustomerDiscountAgg;
using Microsoft.Extensions.DependencyInjection;
using System;
using ColleagueDiscountManagementApplication.Contract.ColleagueDiscount;
using DiscountManagementInfrastructure.EfCore;
using DiscountManagementInfrastructure.EfCore.Repository;
using Microsoft.EntityFrameworkCore;
using DiscountManagement.Domain.ColleagueDiscountAgg;
using DiscountManagement.Application;
using ColleagueDiscountManagementApplication.Contract.CustomerDiscount;

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

            services.AddDbContext<DiscountContext>(option =>
            {
                option.UseSqlServer(connectionString);
            });
        }
    }
}
