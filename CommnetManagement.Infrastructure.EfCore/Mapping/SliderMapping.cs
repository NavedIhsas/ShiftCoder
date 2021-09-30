using CommentManagement.Domain.SliderAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommentManagement.Infrastructure.EfCore.Mapping
{
    class SliderMapping:IEntityTypeConfiguration<Slider>
    {
        public void Configure(EntityTypeBuilder<Slider> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).HasMaxLength(70);
            builder.Property(x => x.ShortTitle).HasMaxLength(50);
            builder.Property(x => x.Picture).HasMaxLength(500);
            builder.Property(x => x.PictureTitle).HasMaxLength(150);
            builder.Property(x => x.PictureAlt).HasMaxLength(150);
            builder.Property(x => x.ButtonText).HasMaxLength(20);
        }
    }
}
