using DiscountManagement.Domain.UserDiscountAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiscountManagementInfrastructure.EfCore.Mapping
{
    public class UserDiscountMapping : IEntityTypeConfiguration<UserDiscount>
    {
        public void Configure(EntityTypeBuilder<UserDiscount> builder)
        {
            builder.HasKey(x => new { x.AccountId, x.DiscountCodeId });
        }
    }
}