using DiscountManagement.Domain.ColleagueDiscountAgg;
using DiscountManagement.Domain.CustomerDiscountAgg;
using DiscountManagementInfrastructure.EfCore.Mapping;
using Microsoft.EntityFrameworkCore;

namespace DiscountManagementInfrastructure.EfCore
{
    public class DiscountContext:DbContext
    {
        public DiscountContext(DbContextOptions<DiscountContext> options):base(options)
        {
            
        }

        public DbSet<CustomerDiscount> CustomerDiscounts { get; set; }
        public DbSet<ColleagueDiscount> ColleagueDiscounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(CustomerMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
