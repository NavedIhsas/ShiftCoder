using Microsoft.EntityFrameworkCore;
using System;
using OrderInfrastructure.EfCore.Mapping;
using OrderManagement.Domain.OrderAgg;
using OrderManagement.Domain.OrderDetailAgg;

namespace OrderInfrastructure.EfCore
{
    public class OrderContext:DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options):base(options)
        {
            
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var assembly = typeof(OrderMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        }
    }
}
