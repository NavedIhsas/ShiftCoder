using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.OrderDetailAgg;

namespace ShopManagement.Infrastructure.EfCore.Mapping
{
   public class OrderDetailsMapping:IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.HasKey(x => new { x.OrderId, x.CourseId });
        }
    }
}
