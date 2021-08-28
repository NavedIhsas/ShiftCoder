using InventoryManagement.Domain.InventoryAgg;
using InventoryManagementInfrastructure.EfCore.Mapping;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementInfrastructure.EfCore
{
   public class InventoryContext:DbContext
    {
        public InventoryContext(DbContextOptions<InventoryContext> options):base(options){}

        public DbSet<Inventory> Inventories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(InventoryMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
