using AccountManagement.Domain.Account.Agg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EfCore.Mapping
{
   public class AccountMapping:IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Users");
            builder.HasQueryFilter(x => x.IsActive);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FullName).HasMaxLength(250).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(250).IsRequired();
            builder.Property(x => x.Avatar).HasMaxLength(500);
            builder.Property(x => x.Password).HasMaxLength(250).IsRequired();
            builder.Property(x => x.Phone).HasMaxLength(20);
            builder.HasOne(x => x.Role).WithMany(x => x.Accounts).HasForeignKey(x => x.RoleId);
            builder.HasMany(x => x.Teachers).WithOne(x => x.Account).HasForeignKey(x => x.AccountId);
        }
    }
}
