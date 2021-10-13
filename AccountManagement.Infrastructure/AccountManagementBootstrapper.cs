using _0_FrameWork.Domain.Infrastructure;
using AccountManagement.Application;
using AccountManagement.Application.Contract.Account;
using AccountManagement.Application.Contract.Role;
using AccountManagement.Domain.Account.Agg;
using AccountManagement.Domain.RoleAgg;
using AccountManagement.Infrastructure.EfCore;
using AccountManagement.Infrastructure.EfCore.Repository;
using AccountManagement.Infrastructure.Permissions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShiftCoderQuery.Contract.Account;
using ShiftCoderQuery.Query;

namespace AccountManagement.Infrastructure
{
    public class AccountManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IAccountApplication, AccountApplication>();
            services.AddTransient<ITeacherRepository, TeacherRepository>();

            services.AddTransient<IRoleApplication, RoleApplication>();
            services.AddTransient<IRoleRepository, RoleRepository>();

            services.AddTransient<IPermissionExposer, AccountPermissionExposer>();
            services.AddTransient<IAccountQuery, AccountQuery>();

            services.AddDbContext<AccountContext>(option => { option.UseSqlServer(connectionString); });
        }
    }
}