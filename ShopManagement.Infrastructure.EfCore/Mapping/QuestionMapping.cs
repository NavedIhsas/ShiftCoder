using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.ForumAgg;

namespace ShopManagement.Infrastructure.EfCore.Mapping
{
   public class QuestionMapping:IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x => x.CourseId).IsRequired();
            builder.HasMany(x => x.Answers).WithOne(x => x.Question).HasForeignKey(x => x.QuestionId);
        }
    }
}
