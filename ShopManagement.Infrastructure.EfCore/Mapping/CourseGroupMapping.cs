using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShopManagement.Infrastructure.EfCore.Mapping
{
  public  class CourseGroupMapping:IEntityTypeConfiguration<Domain.CourseGroupAgg.CourseGroup>
    {
        public void Configure(EntityTypeBuilder<Domain.CourseGroupAgg.CourseGroup> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).HasMaxLength(200).IsRequired();
            builder.Property(x => x.KeyWords).HasMaxLength(100).IsRequired();
            builder.Property(x => x.MetaDescription).HasMaxLength(150).IsRequired();
            builder.Property(x => x.Slug).HasMaxLength(200).IsRequired();
        }
    }
}
