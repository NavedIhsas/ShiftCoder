using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.CourseGroupAgg;

namespace ShopManagement.Infrastructure.EfCore.Mapping
{
    public class CourseGroupMapping : IEntityTypeConfiguration<CourseGroup>
    {
        public void Configure(EntityTypeBuilder<CourseGroup> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).HasMaxLength(200).IsRequired();
            builder.Property(x => x.KeyWords).HasMaxLength(100).IsRequired();
            builder.Property(x => x.MetaDescription).HasMaxLength(150).IsRequired();
            builder.Property(x => x.Slug).HasMaxLength(200).IsRequired();
            builder.Property(x => x.PictureTitle).HasMaxLength(200);
            builder.Property(x => x.PictureAlt).HasMaxLength(200);
            builder.HasMany(x => x.Groups).WithOne(x => x.SubGroup).HasForeignKey(x => x.SubGroupId);
        }
    }
}