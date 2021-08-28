using InventoryManagement.Application.Contract.Inventory;
using Microsoft.Extensions.DependencyInjection;
using InventoryManagement.Application;
using InventoryManagement.Domain.InventoryAgg;
using InventoryManagementInfrastructure.EfCore;
using InventoryManagementInfrastructure.EfCore.Repository;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure
{
    public class InventoryManagementBootstrapper
    {
        public static void Configure(IServiceCollection service, string connectionString)
        {
            service.AddTransient<IInventoryApplication, InventoryApplication>();
            service.AddTransient<IInventoryRepository, InventoryRepository>();

            service.AddDbContext<InventoryContext>(option =>
            {
                option.UseSqlServer(connectionString);
            });
        }
    }
}
