using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.ForumAgg;

namespace ShopManagement.Infrastructure.EfCore.Mapping
{
    public class AnswerMapping : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x => x.QuestionId).IsRequired();
            builder.HasOne(x => x.Question).WithMany(x => x.Answers).HasForeignKey(x => x.QuestionId);
        }
    }
}