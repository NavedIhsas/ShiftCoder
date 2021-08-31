using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.CourseEpisodeAgg;

namespace ShopManagement.Infrastructure.EfCore.Mapping
{
   public class CourseEpisodeMapping:IEntityTypeConfiguration<CourseEpisode>
    {
        public void Configure(EntityTypeBuilder<CourseEpisode> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.FileName).HasMaxLength(1000);
            builder.Property(x => x.KeyWords).HasMaxLength(100).IsRequired();
            builder.Property(x => x.MetaDescription).HasMaxLength(150).IsRequired();
            builder.Property(x => x.Time).IsRequired();
            builder.HasOne(x => x.Course).WithMany(x => x.CourseEpisodes).HasForeignKey(x => x.CourseId);
        }
    }
}
