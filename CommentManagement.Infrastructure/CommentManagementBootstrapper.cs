using CommentManagement.Domain.CourseCommentAgg;
using Microsoft.Extensions.DependencyInjection;
using System;
using CommentManagement.Application.Contract.Comment;
using CommentManagement.Infrastructure.EfCore;
using CommentManagement.Infrastructure.EfCore.Repository;
using CommentManagement.Application;
using CommentManagement.Domain.VisitAgg;
using Microsoft.EntityFrameworkCore;
using ShiftCoderQuery.Contract.Comment;
using ShiftCoderQuery.Query;

namespace CommentManagement.Infrastructure
{
    public class CommentManagementBootstrapper
    {
        public static void Configure(IServiceCollection service, string connectionString)
        {
            service.AddTransient<ICommentRepository, CommentRepository>();
            service.AddTransient<ICommentApplication, CommentApplication>();
            service.AddTransient<ICommentQuery, CommandQuery>();
          
            service.AddTransient<IVisitRepository, VisitRepository>();


            service.AddDbContext<CommentContext>(option =>
            {
                option.UseSqlServer(connectionString);
            });
        }
    }
}
