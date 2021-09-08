using AccountManagement.Domain.Account.Agg;
using AccountManagement.Infrastructure.EfCore.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using AccountManagement.Application.Contract.Account;
using AccountManagement.Application;
using AccountManagement.Application.Contract.Role;
using AccountManagement.Domain.RoleAgg;
using AccountManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure
{
    public class AccountManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IAccountApplication, AccountApplication>();

            services.AddTransient<IRoleApplication, RoleApplication>();
            services.AddTransient<IRoleRepository, RoleRepository>();

            services.AddDbContext<AccountContext>(option =>
            {
                option.UseSqlServer(connectionString);
            });
        }
    }
}
