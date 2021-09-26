using CommentManagement.Domain.CourseCommentAgg;
using CommentManagement.Domain.HomePageDetailsAgg;
using CommentManagement.Domain.Notification.Agg;
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
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<HomePhoto> HomePhotos { get; set; }
        public DbSet<News> News { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(CommentMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
