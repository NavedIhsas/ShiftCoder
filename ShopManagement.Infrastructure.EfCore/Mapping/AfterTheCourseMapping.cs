using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.AfterTheCourseAgg;

namespace ShopManagement.Infrastructure.EfCore.Mapping
{
    public class AfterTheCourseMapping : IEntityTypeConfiguration<AfterTheCourse>
    {
        public void Configure(EntityTypeBuilder<AfterTheCourse> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).HasMaxLength(1000).IsRequired();
            builder.HasOne(x => x.Courses).WithMany(x => x.AfterTheCourses).HasForeignKey(x => x.CourseId);

        }
    }
}