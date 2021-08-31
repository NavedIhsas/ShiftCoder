using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.CoursePrerequisiteAgg;

namespace ShopManagement.Infrastructure.EfCore.Mapping
{
    public class CoursePrerequisiteMapping : IEntityTypeConfiguration<CoursePrerequisite>
    {
        public void Configure(EntityTypeBuilder<CoursePrerequisite> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).HasMaxLength(1000).IsRequired();
            builder.HasOne(x => x.Courses).WithMany(x => x.CoursePrerequisites).HasForeignKey(x => x.CourseId);
        }
    }
}