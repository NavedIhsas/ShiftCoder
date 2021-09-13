using CommentManagement.Domain.CourseCommentAgg;
using CommentManagement.Domain.VisitAgg;
using CommentManagement.Infrastructure.EfCore.Mapping;
using Microsoft.EntityFrameworkCore;

namespace CommentManagement.Infrastructure.EfCore
{
    public class CommentContext:DbContext 
    {
        public CommentContext(DbContextOptions<CommentContext> options):base(options)
        {
            
        }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Visit> Visits { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(CommentMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
