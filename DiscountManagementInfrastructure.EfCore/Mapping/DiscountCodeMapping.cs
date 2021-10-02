using DiscountManagement.Domain.DiscountCode;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiscountManagementInfrastructure.EfCore.Mapping
{
    public class DiscountCodeMapping : IEntityTypeConfiguration<DiscountCode>
    {
        public void Configure(EntityTypeBuilder<DiscountCode> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.DiscountRate).IsRequired();
            builder.Property(x => x.Reason).HasMaxLength(250);
            builder.Property(x => x.Code).HasMaxLength(50).IsRequired();
        }
    }
}