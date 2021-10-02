using AccountManagement.Domain.Account.Agg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EfCore.Mapping
{
    public class TeacherMapping : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.ToTable("UserTeachers");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Bio).HasMaxLength(500);
            builder.Property(x => x.Resumes).HasMaxLength(1000);
            builder.Property(x => x.Skills).HasMaxLength(250);
            builder.HasOne(x => x.Account).WithMany(x => x.Teachers).HasForeignKey(x => x.AccountId);
        }
    }
}