using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.CourseStatusAgg;

namespace ShopManagement.Infrastructure.EfCore.Mapping
{
    public class CourseStatusMapping : IEntityTypeConfiguration<CourseStatus>
    {
        public void Configure(EntityTypeBuilder<CourseStatus> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).HasMaxLength(200).IsRequired();
            builder.HasData(new CourseStatus { Title = "در حال برگذاری", Id = 1 });
            builder.HasData(new CourseStatus { Title = "تشکیل شده", Id = 2 });
            builder.HasData(new CourseStatus { Title = "اتمام", Id = 3 });
        }
    }
}