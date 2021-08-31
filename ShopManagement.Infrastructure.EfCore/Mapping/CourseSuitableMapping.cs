using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.CourseSuitableAgg;

namespace ShopManagement.Infrastructure.EfCore.Mapping
{
   public class CourseSuitableMapping: IEntityTypeConfiguration<CourseSuitable>
    {
        public void Configure(EntityTypeBuilder<CourseSuitable> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).HasMaxLength(1000).IsRequired();
            builder.HasOne(x => x.Courses).WithMany(x => x.CourseSuitableList).HasForeignKey(x => x.CourseId);

        }
    }
}
